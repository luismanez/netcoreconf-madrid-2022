import { AccessTokenProvider, AllowedHostsValidator } from '@microsoft/kiota-abstractions';
import { AadTokenProvider } from '@microsoft/sp-http';

export class SpfxAadTokenProvider implements AccessTokenProvider {

    private readonly allowedHostsValidator: AllowedHostsValidator;

    constructor(
        public readonly tokenProvider: AadTokenProvider,
        public readonly applicationIdUri: string,
        allowedHosts: Set<string>) {
            this.allowedHostsValidator = new AllowedHostsValidator(allowedHosts);
    }

    public async getAuthorizationToken(
        url?: string, 
        additionalAuthenticationContext?: Record<string, unknown>): Promise<string> {
        return await this.tokenProvider.getToken(this.applicationIdUri);
    }    

    public getAllowedHostsValidator = () => this.allowedHostsValidator;
}