import { HttpClient } from 'aurelia-fetch-client';
import { inject, bindable } from 'aurelia-framework';
import { Filter } from './filter';

@inject(HttpClient)
export class RequestList {

    @bindable searchTerm = '';

    constructor(httpClient) {

        this.imageUrl = window.imagePath;
        this.priorities = window.priorities;

        this.message = 'Show List!';
        this.httpClient = httpClient;
        this.filter = new Filter(this);
        this.requestList = [];

        this.searchRequests();
    }

    searchRequests() {
        var url = 'donationdata/requests';
        var params = [];

        if (this.searchTerm) {
            params.push('searchTerms=' + encodeURIComponent(this.searchTerm));
        }
        var additionalArguments = this.filter.getUrlArguments();
        additionalArguments.forEach(function(element) {
            params.push(element);
        }, this);

        this.updateRequests(url, params);
    }

    updateRequests(url, params) {

        if (params !== undefined) {
            params = params.join('&');

            if (params) {
                url = url + '?' + params;
            }
        }

        this.httpClient.fetch(url)
            .then(response => response.json())
            .then(data => {
                this.requestList = data;
                this.requestList.forEach(function(request) {
                    var sumOfDonations = 0;
                    request.donations.forEach(function(donation) {
                        sumOfDonations = sumOfDonations + donation.amountUSD;
                    }, this);
                    request.stillNeeded = request.neededAmountUSD - sumOfDonations;
                }, this);
            });
    }

    searchTermChanged(newVal, oldVal) {
        this.searchRequests(newVal);
    }
}