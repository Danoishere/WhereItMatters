define('app',['exports'], function (exports) {
  'use strict';

  Object.defineProperty(exports, "__esModule", {
    value: true
  });

  function _classCallCheck(instance, Constructor) {
    if (!(instance instanceof Constructor)) {
      throw new TypeError("Cannot call a class as a function");
    }
  }

  var App = exports.App = function App() {
    _classCallCheck(this, App);

    this.message = 'Hello World!';
  };
});
define('donationrequest',["exports"], function (exports) {
    "use strict";

    Object.defineProperty(exports, "__esModule", {
        value: true
    });

    function _classCallCheck(instance, Constructor) {
        if (!(instance instanceof Constructor)) {
            throw new TypeError("Cannot call a class as a function");
        }
    }

    var DonationRequest = exports.DonationRequest = function DonationRequest() {
        _classCallCheck(this, DonationRequest);
    };
});
define('environment',["exports"], function (exports) {
  "use strict";

  Object.defineProperty(exports, "__esModule", {
    value: true
  });
  exports.default = {
    debug: true,
    testing: true
  };
});
define('filter',['exports', 'aurelia-framework'], function (exports, _aureliaFramework) {
    'use strict';

    Object.defineProperty(exports, "__esModule", {
        value: true
    });
    exports.Filter = undefined;

    function _initDefineProp(target, property, descriptor, context) {
        if (!descriptor) return;
        Object.defineProperty(target, property, {
            enumerable: descriptor.enumerable,
            configurable: descriptor.configurable,
            writable: descriptor.writable,
            value: descriptor.initializer ? descriptor.initializer.call(context) : void 0
        });
    }

    function _classCallCheck(instance, Constructor) {
        if (!(instance instanceof Constructor)) {
            throw new TypeError("Cannot call a class as a function");
        }
    }

    function _applyDecoratedDescriptor(target, property, decorators, descriptor, context) {
        var desc = {};
        Object['ke' + 'ys'](descriptor).forEach(function (key) {
            desc[key] = descriptor[key];
        });
        desc.enumerable = !!desc.enumerable;
        desc.configurable = !!desc.configurable;

        if ('value' in desc || desc.initializer) {
            desc.writable = true;
        }

        desc = decorators.slice().reverse().reduce(function (desc, decorator) {
            return decorator(target, property, desc) || desc;
        }, desc);

        if (context && desc.initializer !== void 0) {
            desc.value = desc.initializer ? desc.initializer.call(context) : void 0;
            desc.initializer = undefined;
        }

        if (desc.initializer === void 0) {
            Object['define' + 'Property'](target, property, desc);
            desc = null;
        }

        return desc;
    }

    function _initializerWarningHelper(descriptor, context) {
        throw new Error('Decorating class property failed. Please ensure that transform-class-properties is enabled.');
    }

    var _desc, _value, _class, _descriptor;

    var Filter = exports.Filter = (_class = function () {
        function Filter(list) {
            _classCallCheck(this, Filter);

            _initDefineProp(this, 'priority', _descriptor, this);

            this.list = list;

            this.priority[0] = window.filterData.checkL1;
            this.priority[1] = window.filterData.checkL2;
            this.priority[2] = window.filterData.checkL3;
            this.priority[3] = window.filterData.checkL4;
            this.priority[4] = window.filterData.checkL5;
            this.priority[5] = window.filterData.checkL6;
        }

        Filter.prototype.getUrlArguments = function getUrlArguments() {
            var url = [];

            this.priority.forEach(function (element) {
                url.push('priority=' + element);
            }, this);

            return url;
        };

        Filter.prototype.updatePriorities = function updatePriorities() {
            this.list.searchRequests();
        };

        return Filter;
    }(), (_descriptor = _applyDecoratedDescriptor(_class.prototype, 'priority', [_aureliaFramework.bindable], {
        enumerable: true,
        initializer: function initializer() {
            return [];
        }
    })), _class);
});
define('main',['exports', './environment'], function (exports, _environment) {
  'use strict';

  Object.defineProperty(exports, "__esModule", {
    value: true
  });
  exports.configure = configure;

  var _environment2 = _interopRequireDefault(_environment);

  function _interopRequireDefault(obj) {
    return obj && obj.__esModule ? obj : {
      default: obj
    };
  }

  Promise.config({
    longStackTraces: _environment2.default.debug,
    warnings: {
      wForgottenReturn: false
    }
  });

  function configure(aurelia) {
    aurelia.use.standardConfiguration().feature('resources');

    if (_environment2.default.debug) {
      aurelia.use.developmentLogging();
    }

    if (_environment2.default.testing) {
      aurelia.use.plugin('aurelia-testing');
    }

    aurelia.start().then(function () {
      return aurelia.setRoot();
    });
  }
});
define('requestlist',['exports', 'aurelia-fetch-client', 'aurelia-framework', './filter'], function (exports, _aureliaFetchClient, _aureliaFramework, _filter) {
    'use strict';

    Object.defineProperty(exports, "__esModule", {
        value: true
    });
    exports.RequestList = undefined;

    function _initDefineProp(target, property, descriptor, context) {
        if (!descriptor) return;
        Object.defineProperty(target, property, {
            enumerable: descriptor.enumerable,
            configurable: descriptor.configurable,
            writable: descriptor.writable,
            value: descriptor.initializer ? descriptor.initializer.call(context) : void 0
        });
    }

    function _classCallCheck(instance, Constructor) {
        if (!(instance instanceof Constructor)) {
            throw new TypeError("Cannot call a class as a function");
        }
    }

    function _applyDecoratedDescriptor(target, property, decorators, descriptor, context) {
        var desc = {};
        Object['ke' + 'ys'](descriptor).forEach(function (key) {
            desc[key] = descriptor[key];
        });
        desc.enumerable = !!desc.enumerable;
        desc.configurable = !!desc.configurable;

        if ('value' in desc || desc.initializer) {
            desc.writable = true;
        }

        desc = decorators.slice().reverse().reduce(function (desc, decorator) {
            return decorator(target, property, desc) || desc;
        }, desc);

        if (context && desc.initializer !== void 0) {
            desc.value = desc.initializer ? desc.initializer.call(context) : void 0;
            desc.initializer = undefined;
        }

        if (desc.initializer === void 0) {
            Object['define' + 'Property'](target, property, desc);
            desc = null;
        }

        return desc;
    }

    function _initializerWarningHelper(descriptor, context) {
        throw new Error('Decorating class property failed. Please ensure that transform-class-properties is enabled.');
    }

    var _dec, _class, _desc, _value, _class2, _descriptor;

    var RequestList = exports.RequestList = (_dec = (0, _aureliaFramework.inject)(_aureliaFetchClient.HttpClient), _dec(_class = (_class2 = function () {
        function RequestList(httpClient) {
            _classCallCheck(this, RequestList);

            _initDefineProp(this, 'searchTerm', _descriptor, this);

            this.imageUrl = window.imagePath;
            this.priorities = window.priorities;

            this.message = 'Show List!';
            this.httpClient = httpClient;
            this.filter = new _filter.Filter(this);
            this.requestList = [];

            this.searchRequests();
        }

        RequestList.prototype.searchRequests = function searchRequests() {
            var url = 'donationdata/requests';
            var params = [];

            if (this.searchTerm) {
                params.push('searchTerms=' + encodeURIComponent(this.searchTerm));
            }
            var additionalArguments = this.filter.getUrlArguments();
            additionalArguments.forEach(function (element) {
                params.push(element);
            }, this);

            this.updateRequests(url, params);
        };

        RequestList.prototype.updateRequests = function updateRequests(url, params) {
            var _this = this;

            if (params !== undefined) {
                params = params.join('&');

                if (params) {
                    url = url + '?' + params;
                }
            }

            this.httpClient.fetch(url).then(function (response) {
                return response.json();
            }).then(function (data) {
                _this.requestList = data;
                _this.requestList.forEach(function (request) {
                    var sumOfDonations = 0;
                    request.donations.forEach(function (donation) {
                        sumOfDonations = sumOfDonations + donation.amountUSD;
                    }, this);
                    request.stillNeeded = request.neededAmountUSD - sumOfDonations;
                    request.stillNeeded = request.stillNeeded.toFixed(2);
                }, _this);
            });
        };

        RequestList.prototype.searchTermChanged = function searchTermChanged(newVal, oldVal) {
            this.searchRequests(newVal);
        };

        RequestList.prototype.strip = function strip(number) {
            return parseFloat(number).toPrecision(12);
        };

        return RequestList;
    }(), (_descriptor = _applyDecoratedDescriptor(_class2.prototype, 'searchTerm', [_aureliaFramework.bindable], {
        enumerable: true,
        initializer: function initializer() {
            return '';
        }
    })), _class2)) || _class);
});
define('requestlistmain',['exports', './environment', 'aurelia-fetch-client'], function (exports, _environment, _aureliaFetchClient) {
    'use strict';

    Object.defineProperty(exports, "__esModule", {
        value: true
    });
    exports.configure = configure;

    var _environment2 = _interopRequireDefault(_environment);

    function _interopRequireDefault(obj) {
        return obj && obj.__esModule ? obj : {
            default: obj
        };
    }

    Promise.config({
        longStackTraces: _environment2.default.debug,
        warnings: {
            wForgottenReturn: false
        }
    });

    function configure(aurelia) {
        aurelia.use.standardConfiguration().feature('resources');

        if (_environment2.default.debug) {
            aurelia.use.developmentLogging();
        }

        if (_environment2.default.testing) {
            aurelia.use.plugin('aurelia-testing');
        }

        var httpClient = new _aureliaFetchClient.HttpClient();

        httpClient.configure(function (config) {
            config.withBaseUrl('api/').withDefaults({
                credentials: 'same-origin',
                headers: {
                    'Accept': 'application/json',
                    'X-Requested-With': 'Fetch'
                }
            });
        });

        aurelia.container.registerInstance(_aureliaFetchClient.HttpClient, httpClient);
        aurelia.start().then(function () {
            return aurelia.setRoot('requestlist');
        });
    }
});
define('resources/index',["exports"], function (exports) {
  "use strict";

  Object.defineProperty(exports, "__esModule", {
    value: true
  });
  exports.configure = configure;
  function configure(config) {}
});
define('text!app.html', ['module'], function(module) { module.exports = "<template>\n  <h1>${message}</h1>\n</template>\n"; });
define('text!donationrequest.html', ['module'], function(module) { module.exports = ""; });
define('text!filter.html', ['module'], function(module) { module.exports = "<template>\r\n    <div class=\"thumbnail\">\r\n        <div style=\"margin:10px\">\r\n            <h3>Filter</h3>\r\n            <div>\r\n                <div class=\"row\">\r\n                <div class=\"col-sm-12\">\r\n                    <h4>Urgency</h4>\r\n                </div>\r\n            </div>\r\n            <div class=\"row\">\r\n                <div class=\"col-sm-12\">\r\n                    <input type=\"checkbox\" checked.bind=\"priority[0]\" click.delegate=\"updatePriorities() & debounce\"/>\r\n                    <label>${priorities.L1}</label>\r\n                </div>\r\n            </div>\r\n            <div class=\"row\">\r\n                <div class=\"col-sm-12\">\r\n                    <input type=\"checkbox\" checked.bind=\"priority[1]\" click.delegate=\"updatePriorities() & debounce\"/>\r\n                    <label>${priorities.L2}</label>\r\n                </div>\r\n            </div>\r\n            <div class=\"row\">\r\n                <div class=\"col-sm-12\">\r\n                    <input type=\"checkbox\" checked.bind=\"priority[2]\" click.delegate=\"updatePriorities() & debounce\"/>\r\n                    <label>${priorities.L3}</label>\r\n                </div>\r\n            </div>\r\n            <div class=\"row\">\r\n                <div class=\"col-sm-12\">\r\n                    <input type=\"checkbox\" checked.bind=\"priority[3]\" click.delegate=\"updatePriorities() & debounce\"/>\r\n                    <label>${priorities.L4}</label>\r\n                </div>\r\n            </div>\r\n            <div class=\"row\">\r\n                <div class=\"col-sm-12\">\r\n                    <input type=\"checkbox\" checked.bind=\"priority[4]\" click.delegate=\"updatePriorities() & debounce\"/>\r\n                    <label>${priorities.L5}</label>\r\n                </div>\r\n            </div>\r\n            <div class=\"row\">\r\n                <div class=\"col-sm-12\">\r\n                    <input type=\"checkbox\" checked.bind=\"priority[5]\" click.delegate=\"updatePriorities() & debounce\"/>\r\n                    <label>${priorities.L6}</label>\r\n                </div>\r\n            </div>\r\n            <div>\r\n        </div>\r\n    </div>\r\n</template>"; });
define('text!requestlist.html', ['module'], function(module) { module.exports = "<template>\r\n  <br/>\r\n  <div class=\"row\">\r\n    <div class=\"col-sm-12\">\r\n      <h2>Search Donation Requests</h2>\r\n    </div>\r\n    </div>\r\n    <div class=\"row\">\r\n        <div class=\"col-sm-12\">\r\n            <div class=\"input-group\">\r\n                <input type=\"text\" class=\"form-control\" style=\"max-width: 100%\" value.bind=\"searchTerm & debounce:500\" placeholder=\"Search for...\">\r\n                <span class=\"input-group-btn\">\r\n                    <button class=\"btn btn-info\"  click.delegate=\"searchRequests(searchTerm)\" type=\"button\">Go!</button>\r\n                </span>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <br/>\r\n    <div class=\"row\">\r\n        <div class=\"col-sm-3\">\r\n            <compose view-model.bind=\"filter\"></compose>\r\n        </div>\r\n        <div class=\"col-sm-9\">\r\n            <div repeat.for=\"request of requestList\">\r\n                <a href=\"/DonationRequest/Overview/${request.id}\" class=\"thumbnail\">\r\n                    <div class=\"row\">\r\n                        <div class=\"col-sm-3\" style=\"margin-bottom: 0\">\r\n                            <img class=\"img img-responsive img-rounded\" src=\"${imageUrl}${request.imageUrl}\" />\r\n                        </div>\r\n                        <div class=\"col-sm-6\">\r\n                            <div class=\"row\"><div class=\"col-sm-12\"><h3>${request.title}</h3></div></div>\r\n                            <div class=\"row\"><div class=\"col-sm-12\"><p style=\"margin-bottom: 0\" class=\"text-italic\">«${request.shortSummary}»</p></div></div>\r\n                        </div>\r\n                        <div class=\"col-sm-3\"  style=\"margin-bottom: 0\">\r\n                            <div class=\"row\"><div class=\"col-sm-12\"><b>${request.donations.length} donors so far<b></div></div>\r\n                            <div class=\"row\"><div class=\"col-sm-12\"><p style=\"margin-bottom: 0\" class=\"text-italic\">$${request.stillNeeded} still needed<p></div></div>\r\n                        </div>\r\n                    </div>\r\n                </a>\r\n                <br />\r\n            </div>\r\n        </div>\r\n    </div>\r\n</template>"; });
//# sourceMappingURL=app-bundle.js.map