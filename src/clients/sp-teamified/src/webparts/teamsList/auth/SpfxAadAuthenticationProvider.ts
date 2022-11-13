import { BaseBearerTokenAuthenticationProvider } from '@microsoft/kiota-abstractions';
import { AadTokenProvider } from '@microsoft/sp-http';
import { SpfxAadTokenProvider } from './SpfxAadTokenProvider';


export class SpfxAadAuthenticationProvider extends BaseBearerTokenAuthenticationProvider {
    public constructor(
        tokenProvider: AadTokenProvider,
        applicationIdUri: string,
        allowedHosts: Set<string> = new Set<string>([
            "localhost",
            "teamifiedapi.azurewebsites.net"
        ])
    ) {
        super(
            new SpfxAadTokenProvider(
                tokenProvider,
                applicationIdUri,
                allowedHosts)
        );
    }
}
