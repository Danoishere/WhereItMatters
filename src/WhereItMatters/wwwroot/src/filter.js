import { inject, bindable } from 'aurelia-framework';

export class Filter {

    @bindable priority = [];

    constructor(list) {

        this.list = list;

        this.priority[0] = true;
        this.priority[1] = false;
        this.priority[2] = false;
        this.priority[3] = false;
        this.priority[4] = false;
        this.priority[5] = false;
    }

    getUrlArguments() {
        var url = [];

        this.priority.forEach(function(element) {
            url.push('priority=' + element);
        }, this);

        return url;
    }

    updatePriorities() {
        this.list.searchRequests();
    }
}