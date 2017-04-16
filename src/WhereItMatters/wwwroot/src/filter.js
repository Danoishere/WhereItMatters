import { inject, bindable } from 'aurelia-framework';

export class Filter {

    @bindable priority = [];

    constructor(list) {

        this.list = list;

        this.priority[0] = window.filterData.checkL1;
        this.priority[1] = window.filterData.checkL2;
        this.priority[2] = window.filterData.checkL3;
        this.priority[3] = window.filterData.checkL4;
        this.priority[4] = window.filterData.checkL5;
        this.priority[5] = window.filterData.checkL6;
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