import environment from './environment';
import { HttpClient } from 'aurelia-fetch-client';

//Configure Bluebird Promises.
Promise.config({
    longStackTraces: environment.debug,
    warnings: {
        wForgottenReturn: false
    }
});

export function configure(aurelia) {
    aurelia.use
        .standardConfiguration()
        .feature('resources');

    if (environment.debug) {
        aurelia.use.developmentLogging();
    }

    if (environment.testing) {
        aurelia.use.plugin('aurelia-testing');
    }

    let httpClient = new HttpClient();

    httpClient.configure(config => {
        config
            .withBaseUrl('api/')
            .withDefaults({
                credentials: 'same-origin',
                headers: {
                    'Accept': 'application/json',
                    'X-Requested-With': 'Fetch'
                }
            });
    });

    aurelia.container.registerInstance(HttpClient, httpClient);
    aurelia.start().then(() => aurelia.setRoot('requestlist'));
}