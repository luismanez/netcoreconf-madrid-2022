import * as React from 'react';
import styles from './TeamsList.module.scss';
import { ITeamsListProps } from './ITeamsListProps';
import { ITeamsListState } from "./ITeamsListState";
import { escape } from '@microsoft/sp-lodash-subset';
import { AadTokenProvider } from '@microsoft/sp-http';
import { FetchRequestAdapter } from '@microsoft/kiota-http-fetchlibrary';
import { AzureAdSpfxAuthenticationProvider } from '@microsoft/kiota-authentication-spfx';
import { TeamifiedApiClient } from '../client/teamifiedApiClient';

export default class TeamsList extends React.Component<ITeamsListProps, ITeamsListState> {

  private readonly azureAdApplicationIdUri: string = "YOUR_AAD_APP_ID_URI!!!!!!";

  constructor(props: ITeamsListProps) {
    super(props);    
    this.state = {
      teams: []
    };
  }

  public componentDidMount(): void {
    this.props.aadTokenProviderFactory.getTokenProvider()
      .then((tokenProvider: AadTokenProvider): void => {

        const authProvider =
          new AzureAdSpfxAuthenticationProvider(
            tokenProvider, 
            this.azureAdApplicationIdUri);
        
        const adapter = new FetchRequestAdapter(authProvider);
        adapter.baseUrl = "https://teamifiedapi.azurewebsites.net"; // or will use the URL defined in the OpenAPI Servers section
        
        const teamifiedClient = new TeamifiedApiClient(adapter);

        teamifiedClient.teams.get().then(teams => {
          console.log(teams);
          this.setState({
            teams: teams
          });
        })
        .catch(e => {console.log(e)});
      })
      .catch(e => {console.log(e)});
  }

  public render(): React.ReactElement<ITeamsListProps> {
    const {
      isDarkTheme,
      environmentMessage,
      hasTeamsContext,
      userDisplayName
    } = this.props;

    const teams = this.state.teams.length > 0 ? <ul>{this.state.teams.map(t => 
        <li key={t.id}><b>{t.displayName}</b>: <i>{t.description}</i></li>)}
      </ul> : <div>Loading data...</div>

    return (
      <section className={`${styles.teamsList} ${hasTeamsContext ? styles.teams : ''}`}>
        <div className={styles.welcome}>
          <img alt="" src={isDarkTheme ? require('../assets/welcome-dark.png') : require('../assets/welcome-light.png')} className={styles.welcomeImage} />
          <h2>Well done, {escape(userDisplayName)}!</h2>
          <div>{environmentMessage}</div>
        </div>
        <div>
          <h3>Teams in your tenant</h3>
          <h4>See DevTools console for all Teams info</h4>
          {teams}
        </div>
      </section>
    );
  }
}
