//! Filename  : ClaroAppCommon.js
//! version   : 1.0.0 (last revision Ene 19, 2017)
//! author    : everis - www.everis.com.
//! copyright : Claro - Americatel Perú
//! www.claro.com.pe
var winTipoSolicitante;
function getConfigBiometria() {
    if (typeof Session.MyWindow2.Session.objBiometria == "undefined") {
        Session.MyWindow2.Session.objBiometria = new Object();
    }
    Session.MyWindow2.Session.objBiometria = Session.objBiometria;
}
(function ($, undefined) {
    var __jControl__ = function (options, $element) {
        var constructor = this.constructor,
            descriptor = constructor.descriptor,
            defaults = constructor.defaults;

        var that = this;

        if (this.__settings == null) {
            this.__settings = {};
        }

        $.each(descriptor.settings.concat(__jControl__.descriptor.settings), function (index, name) {
            var value = options[name];

            if (value == null || value == undefined) {
                value = defaults[name];
            }

            that.__settings[name] = value;
        });

        if (this.__events == null) {
            this.__events = {};
        }

        $.each(descriptor.events.concat(__jControl__.descriptor.events), function (index, name) {
            var value = options[name];

            if (value == null || value == undefined) {
                value = defaults[name];
            }

            that.__events[name] = value;
        });

        for (var member in __jControl__.prototype) {
            if (this[member] == null) {
                this[member] = __jControl__.prototype[member];
            }
        };

        $element = $element || $('<' + this._tagName + '>');

        var id = this.getId();

        if (!$.string.isEmptyOrNull(id)) {
            $element.attr('id', id);
        }

        var data = options.data;

        if (data != null) {
            for (var item in data) {
                $element.attr('data-' + item, data[item]);
            }
        }

        this._setElement($element);

        this.init();
    }

    __jControl__.prototype = {
        init: function (sender, args) {
            this.render();
        },
        render: function (sender, args) {

        },
        add: function (control, $container) {
            if (control == null) {
                throw 'no se ha especificado el control.';
            }

            $container = $container || this.getElement();

            if ($container == null) {
                throw 'El control no se ha especificado el elemento.';
            }

            if (control.id == null || control.id.length == 0) {
                //Generar AutoID
            }

            $container.append(control.getElement());

            control._setParent(this);
        },
        remove: function (control) {
        },
        getParent: function () {
            return this.parent;
        },
        getId: function () {
            return this._getSettings('id');
        },
        getClass: function () {
            return this._getSettings('class');
        },
        getElement: function () {
            return this.__$element;
        },
        _getSettings: function (value) {
            return this.__settings[value];
        },
        _getEvents: function (value) {
            return this.__events[value];
        },
        _setParent: function (control) {
            this.parent = control;
        },
        _setElement: function (value) {
            this.__$element = value;
        },
        _setSettings: function (name, value) {
            this.__settings[name] = value;
        },
        _tagName: null,
        __$element: null,
        __settings: {},
        __events: {}
    }
    __jControl__.defaults = {
        id: '',
        parent: null,
        $element: null
    }
    __jControl__.descriptor = {
        settings: ['id', 'class'],
        events: []
    }

    var __jControlButton__ = function (options, $element) {
        __jControl__.call(this, options, $element);

        var $button = this.getElement();

        if (this._getEvents('click') != null) {
            $button.addEvent(this, 'click', this.click);
        }

        var text = this.getText() || this.getHtml();

        if (text != null && text.length > 0) {
            $button.text(text);
        }

        var cssClass = this.getClass() || 'btn transaction-button btn-sm';

        $button
            .addClass('btn')
            .attr('type', 'button');

        if (cssClass != null && cssClass.length > 0) {
            $button.addClass(cssClass);
        }
    }
    __jControlButton__.prototype = {
        _tagName: 'button',
        constructor: __jControlButton__,
        click: function (sender, args) {
            this._getEvents('click').call(this.getParent(), sender, args);
        },
        getText: function () {
            return this._getSettings('text');
        },
        getHtml: function () {
            return this._getSettings('html');
        }
    }
    __jControlButton__.defaults = {
        text: '',
        html: '',
        click: null,
        dblClick: null
    }
    __jControlButton__.descriptor = {
        settings: ['text', 'html'],
        events: ['click', 'dblclick']
    }

    var __jControlWindow__ = function (options, $element) {
        __jControl__.call(this, options, $element);

        var id = this.getId();

        if ($.string.isEmptyOrNull(id)) {
            id = string.format('window-{0}', $.guid);
        }

        this.getElement().attr('id', id);
        this._setSettings('id', id);
    }

    __jControlWindow__.prototype = {
        _tagName: 'div',
        constructor: __jControlWindow__,
        _createElement: function (className, $container) {
            var $element = $(string.format('.{0}', className), $container);

            if ($element.length === 0) {
                $element = $('<div>');

                $element
                    .addClass(className)
                    .appendTo($container);
            } else {
                throw string.format('The {0} element already exists', className);
            }

            return $element;
        },
        _createDialogElement: function ($container) {
            return this._createElement(__jControlWindow__.dialogClass, $container);
        },
        _createContentElement: function ($container) {
            return this._createElement(__jControlWindow__.contentClass, $container);
        },
        _createBodyElement: function ($container) {
            return this._createElement(__jControlWindow__.bodyClass, $container);
        },
        _createHeaderElement: function ($container) {
            var $header = this._createElement(__jControlWindow__.headerClass, $container);

            this._createHeaderControlboxElement($header);
            this._createHeaderTitleElement($header);

            return $header;
        },
        _createHeaderTitleElement: function ($container) {
            var title = this.getTitle();

            if (!$.string.isEmptyOrNull(title)) {
                this._createElement(__jControlWindow__.titleClass, $container).text(title);
            }
        },
        _createHeaderControlboxElement: function ($container) {
            var that,
                $controlBox,
                buttons;

            buttons = __jControlWindow__.controlBox;

            if (this.getControlBox() && !$.array.isEmptyOrNull(buttons)) {
                var modal = this.getModal();

                $controlBox = this._createElement('modal-controlbox', $container);

                $controlBox.addClass('pull-right');

                var $button;

                if (!modal && this.getMinimizeBox()) {
                    $button = new $.control.button(buttons['minimize']);
                    this.add($button, $controlBox);
                }

                //if (this.getMaximizeBox()) {
                //    $button = new $.control.button(buttons['maximize']);
                //    this.add($button, $controlBox);
                //}

                //$button = new $.control.button(buttons['close']);
                //this.add($button, $controlBox);
            } else {
                $controlBox = null;
            }
        },
        _createFooterElement: function ($container) {
            var that,
                $footer,
                buttons = this.getButtons();

            $footer = this._createElement(__jControlWindow__.footerClass, $container);

            if (buttons != null) {
                that = this;

                $.each(buttons, function (index, button) {
                    if ($.string.isEmptyOrNull(button.text) && $.string.isEmptyOrNull(button.html)) {
                        button.text = index;
                    }

                    var $button = new $.control.button(button);

                    that.add($button, $footer);
                });
            }

            return $footer;
        },
        _resizableStart: function (args) {
            this._maximized = args.maximize;

            var maximize = (this._maximized == null || this._maximized == false),
                            $modal = this.getElement(),
                            $dialog = $('> .modal-dialog', $modal),
                            $content = $('> .modal-content', $dialog),
                            $header = $('> .modal-header', $content),
                            $controlbox = $('> .modal-controlbox', $header),
                            $btnResize = $('#window-maximize', $controlbox),
                            cssPrefix = 'fa fa-window-',
                            oldClass,
                            newClass;

            if (maximize) {
                oldClass = 'restore';
                newClass = 'maximize';
            } else {
                oldClass = 'maximize';
                newClass = 'restore';
            }

            $btnResize
                .removeClass(cssPrefix + oldClass)
                .addClass(cssPrefix + newClass);

            var t = setTimeout(function () {
                $('div.dataTables_scrollHeadInner')
							.css('width', '')
							.find('> table')
								.css('width', '');
                clearTimeout(t);
            }, 100);

        },
        _resizableResize: function (args) {
            var $modal = args.$modal,
                $header = args.$header,
                $body = args.$body,
                $footer = args.$footer,
                headerHeight = ($header != null && $header.length > 0 ? $header.outerHeight() : 0),
                footerHeight = ($footer != null && $footer.length > 0 ? $footer.outerHeight() : 0),
                bodyHeight = ($modal.outerHeight() - (headerHeight + footerHeight))

            $body.css('height', bodyHeight);
        },
        _resize: function () {
            var maximize = (this._maximized == null || this._maximized == false),
                $modal = this.getElement(),
                $dialog = $('> .modal-dialog', $modal),
                $content = $('> .modal-content', $dialog),
                $header = $('> .modal-header', $content),
                $controlbox = $('> .modal-controlbox', $header),
                $btnResize = $('#window-maximize', $controlbox),
                $body = $('> .modal-body', $content),
                $footer = $('> .modal-footer', $content),
                cssPrefix = 'fa-window-',
                oldClass,
                newClass,
                position = { left: 0, top: 0 },
                size = { width: '100%', height: '100%' },
                that = this;

            if (maximize) {
                oldClass = 'maximize';
                newClass = 'restore';

                this._oldPosition = $modal.position();
                this._oldSize = { height: $modal.outerHeight(), width: $modal.outerWidth() };
            } else {
                oldClass = 'restore';
                newClass = 'maximize';

                position = this._oldPosition;
                size = this._oldSize;
            }

            $modal.animate({
                left: position.left,
                top: position.top,
                width: size.width,
                height: size.height
            }, {
                duration: 100,
                start: function () { that._resizableStart.call(that, { maximize: maximize }); },
                progress: function () { that._resizableResize.call(that, { $modal: $modal, $header: $header, $body: $body, $footer: $footer }); }
            });
        },
        _focus: function () {
            if (__jControlWindow__.overlay.length > 0) {
                $('.modal-backdrop').css('z-index', $.app.getMaxZIndex() + 1);
            }

            var $modal = this.getElement(),
                zIndex = $modal.css('z-index'),
                maxZIndex = $.app.getMaxZIndex();

            if (zIndex != maxZIndex) {
                $modal.css('z-index', maxZIndex + 1);
            }
        },
        _initializePosition: function () {
            var $modal = this.getElement();

            $modal
            .offset({
                top: (($(window).height() - $modal.outerHeight()) / 2),
                left: (($(window).width() - $modal.outerWidth()) / 2)
            });
        },
        _showModal: function (args) {
            this._focus();

            if (this._getAutoSize() == true) {
                var $modal = args.$modal,
                                $header = args.$header,
                                $body = args.$body,
                                $footer = args.$footer;

                $body.css('height', '');

                var headerHeight = ($header != null && $header.length > 0 ? $header.outerHeight() : 0),
                    footerHeight = ($footer != null && $footer.length > 0 ? $footer.outerHeight() : 0),
                    bodyHeight = $body.outerHeight();

                $modal.css('height', headerHeight + bodyHeight + footerHeight);
            }

            this._resizableResize(args);
            this._initializePosition();
        },
        open: function () {
            var $modal = this.getElement(),
                $dialog = this._createDialogElement($modal),
                $content = this._createContentElement($dialog),
                $header = this._createHeaderElement($content),
                $body = this._createBodyElement($content),
                $footer = this._createFooterElement($content),
                currentWidth = this.getWidth(),
                currentHeight = this.getHeight(),
                that = this,
                url = this.getUrl();

            if (!$.isNumeric(currentWidth)) {
                currentWidth = currentWidth.replace('px', '');
            }

            if (!$.isNumeric(currentHeight)) {
                currentHeight = currentHeight.replace('px', '');
            }

            var maxWidth = ($(window).innerWidth() > currentWidth ? this.getWidth() : $(window).innerWidth()),
                maxHeight = ($(window).innerHeight() > currentHeight ? this.getHeight() : $(window).innerHeight());

            $modal.addEvent(this, 'shown.bs.modal', function () {
                if (!$.string.isEmptyOrNull(url)) {
                    that._showModal({ $modal: $modal, $header: $header, $body: $body, $footer: $footer });
                    $content.addClass(__jControlWindow__.loadingClass);

                    $.ajax({
                        type: that.getType(),
                        url: url,
                        data: that.getData(),
                        async: true,
                        cache: false,
                        complete: function (jqXHR, textStatus) {
                            //console.logjqXHR);
                            $content.removeClass(__jControlWindow__.loadingClass);
                            that._showModal({ $modal: $modal, $header: $header, $body: $body, $footer: $footer });
                            that.complete();
                        },
                        success: function (data, textStatus, jqXHR) {
                            $body.html(data);
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            //console.logjqXHR);
                            $body.html(errorThrown);
                        }
                    });
                } else {
                    $body.html(this.getText());

                    var t = setTimeout(function () {
                        that._showModal({ $modal: $modal, $header: $header, $body: $body, $footer: $footer });
                        that.complete();
                        clearTimeout(t);
                    }, 1);
                }

                if (this.getModal() == true) {
                    __jControlWindow__.overlay.push(this);
                }
            });

            $header.addEvent(this, 'dblclick', function () {
                //this._resize();
                //this._focus();
            });

            $modal
                .css({
                    width: maxWidth,
                    height: maxHeight
                });

            this._initializePosition();

            $modal
                .attr('url', url)
                .addClass(__jControlWindow__.class)
                .appendTo(document.body)
                .addEvent(this, 'mousedown', function () {
                    this._focus();
                })
                .modal({
                    show: true,
                    backdrop: (__jControlWindow__.overlay.length == 0 ? this.getModal() : false),
                    keyboard: false
                })
                .draggable({
                    handle: $header,
                    containment: document.body
                })
                .resizable({
                    handles: 'n, e, s, w, ne, se, sw, nw',
                    minHeight: 153,
                    minWidth: 148,
                    containment: document.body,
                    start: function () { that._resizableStart.call(that, { maximize: false }); },
                    resize: function () { that._resizableResize.call(that, { $modal: $modal, $header: $header, $body: $body, $footer: $footer }); }
                });
        },

        close: function () {
            this.getElement().remove();

            var count = __jControlWindow__.overlay.length;

            __jControlWindow__.overlay.splice(count - 1, 1);

            var $backdrop = $('> .modal-backdrop', document.body);

            if (__jControlWindow__.overlay.length == 0) {
                $backdrop.remove();
            }

            if (__jControlWindow__.overlay.length > 0) {
                var x = __jControlWindow__.overlay[__jControlWindow__.overlay.length - 1].getElement().css('z-index');

                $backdrop.css('z-index', x - 1);
            }

            var $modals = __jControlWindow__.modals;

            if (!$.array.isEmptyOrNull($modals)) {
                for (var i = 0, j = $modals.length; i < j; i++) {
                    var $modal = $modals[i];

                    if ($modal.getId() == this.getId()) {
                        __jControlWindow__.modals.splice(i, 1);
                    }
                }
            }
        },
        maximize: function () {
            this._resize();
        },
        minimize: function () {
            var $toolbar = __jControlWindow__.toolbar;

            if ($toolbar.length == 1) {
                var $element = this.getElement();

                $element.hide();

                var $li = $('<li>'),
                    $a = $('<a>');

                $a
                    .addClass('glyphicon glyphicon-comment')
                    .attr('title', this.getTitle());

                $a.addEvent(this, 'click', function (e) {
                    $element.show();
                    $li.remove();
                    this._focus();
                });

                $toolbar.append($li.append($a))
            }
        },
        complete: function () {
            if (this._getEvents('complete') != null) {
                this._getEvents('complete').call(this);
            }
        },
        restore: function () {
            this._resize();
        },
        getTarget: function () {
            return this._getSettings('target');
        },
        getAsync: function () {
            return this._getSettings('async');
        },
        getData: function () {
            return this._getSettings('data');
        },
        getType: function () {
            return this._getSettings('type');
        },
        getWidth: function () {
            return this._getSettings('width');
        },
        getHeight: function () {
            return this._getSettings('height');
        },
        getTitle: function () {
            return this._getSettings('title');
        },
        getText: function () {
            return this._getSettings('text');
        },
        getModal: function () {
            return this._getSettings('modal');
        },
        getUrl: function () {
            return this._getSettings('url');
        },
        _getAutoSize: function () {
            return this._getSettings('autoSize');
        },
        getControlBox: function () {
            return this._getSettings('controlBox');
        },
        getMaximizeBox: function () {
            return this._getSettings('maximizeBox');
        },
        getMinimizeBox: function () {
            return this._getSettings('minimizeBox');
        },
        getButtons: function () {
            return this._getSettings('buttons');
        }
    }

    __jControlWindow__.controlBox = {
        minimize: {
            enable: true,
            class: 'fa fa-window-minimize',
            click: function (e) {
                this.minimize();
            }
        },
        maximize: {
            enable: true,
            id: 'window-maximize',
            class: 'fa fa-window-maximize',
            click: function (sender, args) {
                this._resize();
            }
        },
        close: {
            enable: true,
            class: 'fa fa-window-close',
            click: function (sender, args) {
                this.close();
            }
        }
    };
    __jControlWindow__.contentClass = 'modal-content';
    __jControlWindow__.dialogClass = 'modal-dialog';
    __jControlWindow__.headerClass = 'modal-header claro-red';
    __jControlWindow__.titleClass = 'modal-title';
    __jControlWindow__.bodyClass = 'modal-body';
    __jControlWindow__.footerClass = 'modal-footer';
    __jControlWindow__.loadingClass = 'modal-loading';
    __jControlWindow__.class = 'modal';
    __jControlWindow__.toolbar = null;
    __jControlWindow__.overlay = [];
    __jControlWindow__.modals = [];
    __jControlWindow__.defaults = {
        autoSize: false,
        type: 'GET',
        async: true,
        data: null,
        target: '',
        url: '',
        title: null,
        modal: false,
        width: '300px',
        height: '160px',
        controlBox: true,
        maximizeBox: true,
        minimizeBox: true,
        buttons: null,
        complete: null,
    }
    __jControlWindow__.descriptor = {
        settings: ['autoSize', 'async', 'data', 'target', 'url', 'data', 'text', 'title', 'type', 'modal', 'width', 'height', 'controlBox', 'maximizeBox', 'minimizeBox', 'buttons'],
        events: ['complete']
    }

    __jControlWindow__.close = function () {
        var ele = $('div.modal.in:last'),
        id = ele.attr("id");

        var $modals = $.window.modals;

        if (!$.array.isEmptyOrNull($modals)) {
            for (var i = 0, j = $modals.length; i < j; i++) {
                var $modal = $modals[i];

                if ($modal.getId() == id) {
                    $modal.close();
                }
            }
        }
    }
    __jControlWindow__.open = function (options) {
        var url = options.url,
            $modals = __jControlWindow__.modals,
            w = null;

        if (!$.string.isEmptyOrNull(url) && !$.array.isEmptyOrNull($modals)) {
            for (var i = 0, j = $modals.length; i < j; i++) {
                w = $modals[i];

                if (url == w.getUrl()) {
                    w._focus();
                    break;
                }

                w = null;
            }
        }

        if (w == null) {
            w = new __jControlWindow__(options);

            __jControlWindow__.modals.push(w);

            w.open();
        }

        return w;
    }

    $.extend($, {
        app: {
            getMaxZIndex: function () {
                var maxZIndex = 0,
                currentZIndex = 0;

                $('*').each(function () {
                    currentZIndex = $(this).css('z-index');

                    if ($.isNumeric(currentZIndex) && currentZIndex > maxZIndex) {
                        maxZIndex = parseInt(currentZIndex);
                    };
                })

                return maxZIndex;

            },
            ajax: function (settings) {
                var beforeSend = settings.beforeSend,
                    success = settings.success,
                    complete = settings.complete;

                settings.beforeSend = function () {
                    if (settings.container != null) {
                        settings.container.html('<div class="ajax-loading"></div>');
                    }

                    if (beforeSend != null) {
                        beforeSend.call(this, arguments[0], arguments[1]);
                    }
                }

                settings.success = function (response) {
                    if (settings.container != null) {
                        settings.container.html(response);
                    }
                    if (success != null) {
                        success.call(this, arguments[0], arguments[1], arguments[2]);
                    }
                }

                settings.complete = function (response) {
                    if (complete != null) {
                        complete.call(this, arguments[0], arguments[1]);
                    }
                }

                return $.ajax(settings);
            },
            date: {
                format: function (date, format) {
                    return $.formatDate(date, format);
                },
                addMonth: function (date, value, settings) {
                    var currentMonth = date.getMonth();
                    var newDate = date.setMonth(currentMonth + value);

                    if (settings != null) {
                        newDate = $.formatDate(newDate, settings);
                    }

                    return newDate;
                },
                limitedDate: function (date) {
                    var fDateToDay = new Date();
                    fDateToDay = this.format(fDateToDay, { format: 'dd/mm/yy' });
                    return date.valueOf() > fDateToDay.valueOf() ? 'disabled' : '';
                }
            },
            ddmmyyyyhhmisstt2Time: function (date) {
                var dateParts = date.split(' '),
                    date_ddmmyyyy = dateParts[0].split('/'),
                    date_hhmmss = dateParts[1].split(':');

                return (new Date(date_ddmmyyyy[2], date_ddmmyyyy[1], date_ddmmyyyy[0], date_hhmmss[0], date_hhmmss[1], (date_hhmmss[2] + (dateParts[2] != null && dateParts[2].toUpperCase() === 'P.M.' ? 12 : 0)))).getTime();
            },
            const: {
                days: ['Domingo', 'Lunes', 'Martes', 'Miercoles', 'Jueves', 'Viernes', 'Sabado'],
                months: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                today: 'Hoy',
                loadingRecords: 'Cargando...',
                processing: 'Procesando...',
                zeroRecords: 'No se encontraron registros coincidentes.',
                emptyTable: 'No hay datos disponibles en la tabla.',
                formatDate: 'd/m/Y',
                actionSuccessClassName: 'action-exito',
                actionErrorClassName: 'action-error',
                messageErrorLoading: 'Estimado usuario, en este momento estamos presentando intermitencia en el aplicativo<br> por favor intentelo otra vez en unos minutos  ...<br /><br /><label class="control-label">Id de Transacción:</label>'
            }
        },
        window: __jControlWindow__,
        control: {
            button: __jControlButton__
        },
        array: {
            isEmptyOrNull: function (value) {
                return /*value instanceof Array &&*/ (value == null || this.isEmpty(value));
            },
            isEmpty: function (value) {
                return value.length === 0;
            }
        },
        string: {
            isEmptyOrNull: function (value) {
                return (typeof value == 'string' && !value.trim()) || typeof value == 'undefined' || value === null;
            }
        },
        getSiteRoot: function () {
            return siteRoot;
        },
        formatDate: function (date, settings) {

            var formatDateTimeDefault = {

                monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio',
              'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre',
                  'Diciembre'],
                monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul',
                                  'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                dayNames: ['Domingo', 'Lunes', 'Martes', 'Miercoles', 'Jueves',
                           'Viernes', 'Sabado'],
                dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab'],
                ampmNames: ['a.m.', 'p.m.'],
                getSuffix: function (num) {
                    if (num > 3 && num < 21) {
                        return 'th';
                    }

                    switch (num % 10) {
                        case 1: return "st";
                        case 2: return "nd";
                        case 3: return "rd";
                        default: return "th";
                    }
                },
                attribute: 'data-datetime',
                formatAttribute: 'data-dateformat',
                format: 'dd/mm/yy gg:ii:ss a'

            };

            if (!date)
                return '';

            if (typeof date != Date) {
                if ((/^\//).test(date))
                    date = eval('new ' + date.replace(/\//g, ''));
                else
                    date = new Date(date);
            }

            settings = $.extend({}, formatDateTimeDefault, settings);

            var format = settings.format;
            var ticksTo1970 = (((1970 - 1) * 365 + Math.floor(1970 / 4)
                                - Math.floor(1970 / 100)
                                + Math.floor(1970 / 400)) * 24 * 60 * 60 * 10000000);

            var output = '';
            var literal = false;
            var iFormat = 0;

            // Check whether a format character is doubled
            var lookAhead = function (match) {
                var matches = (iFormat + 1 < format.length
                               && format.charAt(iFormat + 1) == match);
                if (matches) {
                    iFormat++;
                }
                return matches;
            };

            // Format a number, with leading zero if necessary
            var formatNumber = function (match, value, len) {
                var num = '' + value;
                if (lookAhead(match)) {
                    while (num.length < len) {
                        num = '0' + num;
                    }
                }
                return num;
            };

            // Format a name, short or long as requested
            var formatName = function (match, value, shortNames, longNames) {
                return (lookAhead(match) ? longNames[value] : shortNames[value]);
            };

            // Get the value for the supplied unit, e.g. year for y
            var getUnitValue = function (unit) {
                switch (unit) {
                    case 'y': return date.getFullYear();
                    case 'm': return date.getMonth() + 1;
                    case 'd': return date.getDate();
                    case 'g': return date.getHours() % 12 || 12;
                    case 'h': return date.getHours();
                    case 'i': return date.getMinutes();
                    case 's': return date.getSeconds();
                    case 'u': return date.getMilliseconds();
                    default: return '';
                }
            };

            for (iFormat = 0; iFormat < format.length; iFormat++) {
                if (literal) {
                    if (format.charAt(iFormat) == "'" && !lookAhead("'")) {
                        literal = false;
                    }
                    else {
                        output += format.charAt(iFormat);
                    }
                } else {
                    switch (format.charAt(iFormat)) {
                        case 'a':
                            output += date.getHours() < 12
                                ? settings.ampmNames[0]
                                : settings.ampmNames[1];
                            break;
                        case 'd':
                            output += formatNumber('d', date.getDate(), 2);
                            break;
                        case 'S':
                            var v = getUnitValue(iFormat && format.charAt(iFormat - 1));
                            output += (v && (settings.getSuffix || $.noop)(v)) || '';
                            break;
                        case 'D':
                            output += formatName('D',
                                                 date.getDay(),
                                                 settings.dayNamesShort,
                                                 settings.dayNames);
                            break;
                        case 'o':
                            var end = new Date(date.getFullYear(),
                                               date.getMonth(),
                                               date.getDate()).getTime();
                            var start = new Date(date.getFullYear(), 0, 0).getTime();
                            output += formatNumber(
                                'o', Math.round((end - start) / 86400000), 3);
                            break;
                        case 'g':
                            output += formatNumber('g', date.getHours() % 12 || 12, 2);
                            break;
                        case 'h':
                            output += formatNumber('h', date.getHours(), 2);
                            break;
                        case 'u':
                            output += formatNumber('u', date.getMilliseconds(), 3);
                            break;
                        case 'i':
                            output += formatNumber('i', date.getMinutes(), 2);
                            break;
                        case 'm':
                            output += formatNumber('m', date.getMonth() + 1, 2);
                            break;
                        case 'M':
                            output += formatName('M',
                                                 date.getMonth(),
                                                 settings.monthNamesShort,
                                                 settings.monthNames);
                            break;
                        case 's':
                            output += formatNumber('s', date.getSeconds(), 2);
                            break;
                        case 'y':
                            output += (lookAhead('y')
                                       ? date.getFullYear()
                                       : (date.getYear() % 100 < 10 ? '0' : '')
                                       + date.getYear() % 100);
                            break;
                        case '@':
                            output += date.getTime();
                            break;
                        case '!':
                            output += date.getTime() * 10000 + ticksTo1970;
                            break;
                        case "'":
                            if (lookAhead("'")) {
                                output += "'";
                            } else {
                                literal = true;
                            }
                            break;
                        default:
                            output += format.charAt(iFormat);
                    }
                }
            }

            return output;
        },
        onlyNumbersPoint: function myfunction(input) {
            input.off('keyup');
            input.keyup(function () {
                if (this.value.match(/[^0-9.]/g)) {
                    this.value = this.value.replace(/[^0-9.]/g, '');
                }
            });
        },
        onlyNumbers: function myfunction(input) {
            input.off('keyup');
            input.keyup(function () {
                if (this.value.match(/[^0-9]/g)) {
                    this.value = this.value.replace(/[^0-9]/g, '');
                }
            });
        },
        onlyNumbersLetters: function myfunction(input) {
            input.off('keyup');
            input.keyup(function () {
                if (this.value.match(/[^a-zA-Z0-9]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z0-9]/g, '');
                }
            });
        },
        onlyNumbersLettersLine: function myfunction(input) {
            input.off('keyup');
            input.keyup(function () {
                if (this.value.match(/[^a-zA-Z0-9-]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z0-9-]/g, '');
                }
            });
        },
        onlyLetters: function myfunction(input) {
            input.off('keyup');
            input.keyup(function () {
                if (this.value.match(/[^a-zA-ZñÑ]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z]/g, '');
                }
            });
        },
        onlyLettersSpaces: function myfunction(input) {
            input.off('keyup');
            input.keyup(function () {
                this.value = this.value.replace(/(\s{2,})|[^a-zA-ZÑñ]/g, ' ');
                this.value = this.value.replace(/^\s*/, '');
            });
        },
        rowSelection: function (options) {
            var _rowSelectionDefault = {
                id: null,
                link: null
            };
            options = $.extend({}, _rowSelectionDefault, options);

            $((options.link == null ? '#' + options.id : '.' + options.link) + ' tbody tr').on('click', function (event) {
                if (event.target.type != 'radio')
                    $(this).find('td input[type=radio]').prop('checked', true).trigger('change');

                $('.' + options.link + ' tbody tr').removeAttr('style');
                $(this).css({ 'background': '#E3EEF7' }).siblings("tr").removeAttr('style');
            });
        }
    });

    $.fn.extend({
        enable: function (value) {
            return this.each(function () {
                this.disabled = (value != undefined ? !value : false);
            });
        },
        populateDropDown: function (items) {
            return this.each(function () {
                var $this = $(this);

                $this.empty();
                $this.enable(false);

                if (items != null && items.length > 0) {
                    $.each(items, function (index, item) {
                        $this.append($("<option>", {
                            value: item.id,
                            text: item.text
                        }));
                    });

                    $this.enable();
                }
            });
        },
        hideMessage: function () {
            return this.each(function () {
                var $that = $(this);

                $that.hide();

                var className = $that.attr('class');

                if (className) {
                    $that.removeClass(className);
                }

                $that.empty();
            });
        },
        showMessage: function (type, message) {
            var className;

            switch (type) {
                case 'error':
                    className = $.app.const.actionErrorClassName;
                    break;
                case 'success':
                    className = $.app.const.actionSuccessClassName;
                    break;
                default:
                    className = '';
                    break;
            }

            return this.each(function () {
                $(this)
                    .addClass(className)
                    .html(message)
                    .show();
            })
        },
        showMessageErrorLoading: function (objectError) {
            var className;
            className = $.app.const.actionErrorClassName;

            var div = $('<div>',
                        {
                            class: 'containerError',
                            align: 'center'
                        });

            if (typeof (objectError) === "object") {

                objectError.message += '<span>' + objectError.session + '</span><br /><br />';
                objectError.message += '<button id="' + objectError.buttonID + '" type="button" class="btn claro-btn-info btn-sm" data-dismiss="modal">Volver a cargar</button>';

                div.html(objectError.message);
                return this.each(function () {
                    $(this)
                        .addClass(className)
                        .html(div)
                        .show();
                    $("#" + objectError.buttonID).addEvent(objectError.that, 'click', objectError.funct);
                    objectError.message = "";
                })
            } else {
                objectError += '<span>' + Session.IDSESSION + '</span><br /><br />';
                div.html(objectError);
                return this.each(function () {
                    $(this)
                        .addClass(className)
                        .html(div)
                        .show();
                })
            }

        },
        addEvent: function (sender, name, event) {
            var fn = this;
            return this.each(function () {
                var that = $(this);
                function facadeEvent(e) {
                    var typeoption2 = that.attr('typeoptions');
                    var args = {};

                    if (typeof typeoption2 != 'undefined' && (typeoption2 == '1' || typeoption2 == '2')) {
                        args = {
                            sender: sender,
                            event: e,
                            control: that,
                            code: that.attr('profile'),
                            fn_response: event,
                            fn_validate: fn.ValidateMenu

                        };

                        $.functionValidateMenu.call(sender, that, args);
                        return;

                    } else {
                        args = {
                            event: e,
                            control: that
                        };
                        event.call(sender, that, args);
                    }
                }


                var atributo = that.attr('profile');
                var typeoption = that.attr('typeoptions');
                if (typeoption == undefined || typeoption == '3') {
                    if (atributo == undefined) {
                        that.unbind(name);
                        that.bind(name, facadeEvent);
                    } else {
                        var stroptionPermissions = Session.USERACCESS.optionPermissions;

                        var arr = atributo.split(",");
                        var i = Session.DATACUSTOMER.Application == 'POSTPAID' ? 0 : Session.DATACUSTOMER.Application == 'HFC' ? 1 : Session.DATACUSTOMER.Application == 'LTE' ? 1 : 0;
                        if (stroptionPermissions.indexOf(arr[i]) < 0) {
                            that.hide();
                        } else {
                            that.unbind(name);
                            that.bind(name, facadeEvent);
                        }

                    }
                } else if (typeoption == '1' || typeoption == '2') {
                    that.unbind(name);
                    that.bind(name, facadeEvent);
                }
            })
        }
    });

    // This will help DataTables magic detect the "ddmmyyyyhhmisstt" format; Unshift
    // so that it's the first data type (so it takes priority over existing)
    jQuery.fn.dataTableExt.aTypes.unshift(
        function (sData) {
            if (/^([0-2]?\d|3[0-1])\/([0-2]?\d|3[0-1])\/\d{4}/i.test(sData)) {
                return 'date-ddmmyyyyhhmisstt';
            }
            return null;
        }
    );

    // define the sorts
    jQuery.fn.dataTableExt.oSort['date-ddmmyyyyhhmisstt-asc'] = function (a, b) {
        var ordA = $.app.ddmmyyyyhhmisstt2Time(a),
            ordB = $.app.ddmmyyyyhhmisstt2Time(b);

        return (ordA < ordB) ? -1 : ((ordA > ordB) ? 1 : 0);
    };

    jQuery.fn.dataTableExt.oSort['date-ddmmyyyyhhmisstt-desc'] = function (a, b) {
        var ordA = $.app.ddmmyyyyhhmisstt2Time(a),
            ordB = $.app.ddmmyyyyhhmisstt2Time(b);

        return (ordA < ordB) ? 1 : ((ordA > ordB) ? -1 : 0);
    };

    jQuery.fn.dataTable.Api.register('sum()', function () {
        return this.flatten().reduce(function (a, b) {
            if (typeof a === 'string') {
                a = a.replace(/[^\d.-]/g, '') * 1;
            }
            if (typeof b === 'string') {
                b = b.replace(/[^\d.-]/g, '') * 1;
            }

            return a + b;
        }, 0);
    });

    $.fn.extend($, {
        IsNullOrUndefinedOrEmpty: function (value) {
            if (!value) {
                if (typeof value == 'undefined' || value == null || value.toString().trim() == '') {
                    return false;
                } else if (value = '') {
                    return false;
                }
                return false;
            }
            if (value.trim() == '') {
                return false;
            }
            return true;
        },
        NameWindow: null,
        Window: {
            open: function (url, args, width, height, center, modal, fn_closed) {
                var left = ((screen.width - parseInt(width)) / 2);
                var top = ((screen.height - parseInt(height)) / 2);
                var options = 'location=si,menubar=no,titlebar=no,resizable=si,toolbar=no, menubar=no,width=' + width + ',height=' + height;
                if (center != null && center == true) options = 'location=si,menubar=no,titlebar=no,resizable=si,toolbar=no, menubar=no,width=' + width + ',height=' + height + ',left=' + left + ',top=' + top;
                var win = null;
                if (modal != null && modal == true) {
                    if (typeof window.showModalDialog != 'undefined') {
                        win = window.showModalDialog(url, args, options);
                    } else {
                        win = window.open(url, '_win', options);
                        win.dialogarguments = args;
                    }
                } else {
                    win = window.open(url, '_win', options);
                    win.dialogarguments = args;
                }
                win.response = null;
                $(win).on('blur', function (e) {
                    win.focus();
                });
                var timer = setInterval(function () {
                    if (typeof win != 'undefined' && win != null) {
                        if (win.closed) {
                            clearInterval(timer);
                            if (typeof fn_closed != 'undefined' && fn_closed != null) {
                                if ($.isFunction(fn_closed)) {
                                    win.response = JSON.parse(win.response);
                                    fn_closed(win.response);
                                }
                            }
                        }
                    }
                }, 1000);
                return win;
            }
        },

        f_MostrarAlertasOContingencia: function (ValOk) {
            var objInfoBioTrazabilidad = Session.objInfoBioTrazabilidad;
           
            if (ValOk == '0') {
                alert(objInfoBioTrazabilidad.JsonRequest.strMensajeValidacionBiometrica1);
                return true;
            }
            else if (ValOk == '3') {
                alert(objInfoBioTrazabilidad.JsonRequest.strMensajeValidacionBiometrica3);
                if (objInfoBioTrazabilidad.ConfiguracionBiometrica.soxpnFlagNoBiometriaReniec == '1') {
                    var strDatosNoBio = f_AbrirNoBiometria(objInfoBioTrazabilidad);
                    if (strDatosNoBio == null || strDatosNoBio == undefined) {
                        return;
                    }
                    else if (strDatosNoBio == '1') {
                        objInfoBioTrazabilidad.ValNoBio = strDatosNoBio;
                        return true;
                    }
                    else {
                        return;
                    }
                }
                else {
                    if (objInfoBioTrazabilidad.ConfiguracionBiometrica.soxpvMensaje != '') {
                        alert(objInfoBioTrazabilidad.ConfiguracionBiometrica.soxpvMensaje);
                    }
                }
                if (objInfoBioTrazabilidad.ConfiguracionBiometrica.soxpnFlagFinVenta == '1') {
                    return true;
                }
                else {
                    return;
                }
            }
            else if (ValOk == '4') {
                alert(objInfoBioTrazabilidad.JsonRequest.strMensajeValidacionBiometrica0);
                return;
            }
            else if (ValOk == '-1') { /*ha cancelado en biometria*/
                return;

            }
            else if (ValOk == '-4') {//discapacidad
                alert(objInfoBioTrazabilidad.JsonRequest.MensajeValidacionBiometricaMenos4);
                if (objInfoBioTrazabilidad.ConfiguracionBiometrica.soxpnFlagNoBiometriaReniec == '1') {
                    var strDatosNoBio = f_AbrirNoBiometria(objInfoBioTrazabilidad);
                    if (strDatosNoBio == null || strDatosNoBio == undefined) {
                        return;
                    }
                    else if (strDatosNoBio == '1') {
                        objInfoBioTrazabilidad.ValNoBio = strDatosNoBio;
                        return true;
                    }
                    else {
                        return;
                    }
                }
                else {
                    if (objInfoBioTrazabilidad.ConfiguracionBiometrica.soxpvMensaje != '') {
                        alert(objInfoBioTrazabilidad.ConfiguracionBiometrica.soxpvMensaje);
                    }
                }
                if (objInfoBioTrazabilidad.ConfiguracionBiometrica.soxpnFlagFinVenta == '1') {
                    return true;
                }
                else {
                    return;
                }
            }
            else if (ValOk == '-2') {//Error de WebMethod
                if (objInfoBioTrazabilidad.ConfiguracionBiometrica.soxpnFlagNoBiometriaReniec == '1') {
                    var strDatosNoBio = f_AbrirNoBiometria(objInfoBioTrazabilidad);
                    if (strDatosNoBio == null || strDatosNoBio == undefined) {
                        return;
                    }
                    else if (strDatosNoBio == '1') {
                        objInfoBioTrazabilidad.ValNoBio = strDatosNoBio;
                        return true;
                    }
                    else {
                        return;
                    }
                }
                else {
                    if (objInfoBioTrazabilidad.ConfiguracionBiometrica.soxpvMensaje != '') {
                        alert(objInfoBioTrazabilidad.ConfiguracionBiometrica.soxpvMensaje);
                    }
                }
                if (objInfoBioTrazabilidad.ConfiguracionBiometrica.soxpnFlagFinVenta == '1') {
                    return true;
                }
                else {
                    return;
                }
            }
            else if (ValOk == '-5') { /*superó el intento de huellas con mala calidad. en biometria*/
                return; //comentar esta return
            }
            else {
                alert(objInfoBioTrazabilidad.JsonRequest.strMensajeValidacionBiometricaOtros);
                return;
            }
        },
        returnResponse: function (response) {

            Session.TypeApplicant.returnvalue = JSON.parse(Session.TypeApplicant.returnvalue);
            alert(Session.TypeApplicant);
            
            //if (Session.InitValidation == '1') {
            //    if (Session.TypeApplicant.returnvalue.length > 1) {
            //        alert('Se ha especificado más de un Representante Legal, se procederá a firmar Manualmente.');
            //        fn_response(Session.TypeApplicant);
            //        return false;
            //    }
            //    else if (Session.TypeApplicant.returnvalue == '-1') {
            //        alert('No se pudo validar correctamente la biometría.');
            //        return false;
            //    } else {
            //        fn_response(Session.TypeApplicant);
            //        return false;
            //    }
            //}
            //else if (Session.InitValidation == '2') {
            //    fn_response(Session.TypeApplicant);
            //    return false;
            //}
        },
        InitValidation: function (Persons, fn_response) {
            if (typeof Session != 'undefined' && Session != null) {
                Session.InitValidation = 'Q';
                Session.TypeApplicant = {
                    returnvalue: null,
                    cantRRLL: 'Q',
                    ListAplicant: []
                };
                var strurl = window.location.protocol + '//' + window.location.host;
                var left = (screen.width - 220) / 2;
                var top = (screen.height - 200) / 2;
                Session.MyWindow = window.open(strurl + '/Transactions/Common/OptionApplicant', '_win', 'menubar=no,titlebar=no,resizable=si,toolbar=no, menubar=no,width=250,height=200,left=' + left +',top=' + top);
                $(Session.MyWindow).on('blur', function (e) { Session.MyWindow.focus(); });

                var timer = setInterval(function () {
                    if (typeof Session.MyWindow != 'undefined' && Session.MyWindow != null) {
                        if (Session.MyWindow.closed) {
                            clearInterval(timer);
                            if (Session.InitValidation == '1' || Session.InitValidation == '2') //1- representante legal , 2- carta poder
                            {
                                var fn = function (varModalTipoSolicitante) {
                                    var modalDialogtop = (screen.height - 157) / 2;
                                    var modalDialogLeft = (screen.width - 325) / 4;
                                    var Persona = Persons;
                                    Persona.OpcionSolicitada = varModalTipoSolicitante;
                                    Session.Persons = Persona;
                                    winTipoSolicitante = window.open(strurl + '/Transactions/Common/TypeApplicant', '_win1', 'titlebar=no,resizable=si,width=480,height=420,top=250,left=500');
                                    $(winTipoSolicitante).on('blur', function (e) { winTipoSolicitante.focus(); });
                                    var timer5 = setInterval(function () {
                                        try {
                                            if (typeof winTipoSolicitante != 'undefined' && winTipoSolicitante != null) {
                                                if (winTipoSolicitante.closed) {
                                                    clearInterval(timer5);
                                                    fn_response(Session.TypeApplicant.returnvalue);
                                                    }
                                                    } else {
                                                        fn_response(Session.TypeApplicant);
                                                        return false;
                                                    }
                                        } catch (ex) {
                                                }
                                    }, 1000);

                                };
                                fn(Session.InitValidation);
                            }

                        } else {
                        }

                    }

                }, 1000);
            }
        },
        ValidateMenu: function (option, parentProduct) {

            if (Session.ORIGINTYPE != '') {
                parentProduct = Session.ORIGINTYPE;
            }
            if (Session.DATASERVICE) {
                if (parentProduct == 'PREPAID' || parentProduct == 'PREPAGO') {
                    if (Session.DATASERVICE.NumberCellphone === 0) {

                        if (option === "LSU_ACPRE_GMP") {


                            alert('Datos del cliente no cargados.\n\nPrimero debe realizar una búsqueda en la parte superior.', Mensaje);
                            return false;
                        }
                    }
                    else {

                        var tempBlackList = Session.DATACUSTOMER.BlackList;

                        var strOpcion = option.substring(0, 10);
                        var strOpcion2 = option.substring(0, 9);
                        if ((option.substring(3, 8) == "SIACA") || (option.substring(3, 7) == "SIAC")) {
                            var EsTFI = Session.DATASERVICE.IsTFI;
                            var ListaOpcTFI = Session.USERACCESS.optionPermissionsMenu.Data.TFI;
                            //console.logoption)
                            //console.logListaOpcTFI.indexOf(option))
                            if (EsTFI === "SI") {
                                if (ListaOpcTFI.indexOf(option) == -1) {
                                    alert('Opción no disponible para FIJO', 'Mensaje');
                                    return false;
                                }
                            }
                        }

                        if (tempBlackList == '0') {
                            if (option !== 'SU_SIAC_DSRE' || option !== 'SU_SIAC_CEDT') {

                                alert('No puede acceder a esta transaccion debido a que la linea se encuentra en estado DESCONOCIDO.', 'Mensaje');
                                return false;
                            }
                        }
                        if (option === 'SU_SIACA_GEO' || option === 'SU_SIACA_ZM' || option === 'SU_SIACA_CEO' || option === 'SU_SIACA_VCO' || option === 'SU_SIAC_3G') {

                            alert('Debe actualizar los datos del clientes', 'Mensaje');
                            return false;
                        }
                        else {

                            if (option === "SU_SIACA_ACDE" || option === "SU_SIACA_CONR" || option === "SU_SIACA_PAQU") {
                                if (tempBlackList == '0') {

                                    alert('No puede acceder a esta transacción debido a que la línea se encuentra en estado DESCONOCIDO', 'Mensaje');
                                    return false;
                                }
                            }
                        }

                        return true;
                    }
                } else if (parentProduct == 'POSTPAID' || parentProduct == 'POSTPAGO' || parentProduct === "DTH" || parentProduct === "HFC" || parentProduct === "LTE") {
                    //VALIDATE LINK
                    var strTitleMessage = "Mensaje";
                    if (option.substr(0, 8) === 'SU_ACP_X') {
                        if (Session.DATASERVICE.CellPhone == '' && Session.DATACUSTOMER.PhoneReference == '') {

                            alert('Seleccione un teléfono de la cuenta', strTitleMessage);
                            return false;
                        }
                    }
                    if (option !== "SU_ACP_AIN" && option !== "SU_ACP_NMD" && option !== "SU_ACP_BLCO" && option !== "SU_ACP_RYC") {
                        if (Session.DATASERVICE.CellPhone === '') {

                            alert('Seleccione un teléfono de la cuenta', strTitleMessage);
                            return false;
                        }
                    }
                    var TypeService = Session.DATASERVICE.TypeService;

                    var arrTPI = Session.USERACCESS.optionPermissionsMenu.Data.TPI;
                    var arrInternet = Session.USERACCESS.optionPermissionsMenu.Data.Internet;
                    var arrFijoPOST = Session.USERACCESS.optionPermissionsMenu.Data.Fijo;
                    var arrHCTNumberOnlyTFI = Session.USERACCESS.optionPermissionsMenu.Data.HCTNumberOnlyTFI;
                    if ($.IsNullOrUndefinedOrEmpty(Session.DATASERVICE.TypeService)) {
                        if ((TypeService === 'TPI' && arrTPI.indexOf(option) < 0) || (TypeService === 'INTERNET' && arrInternet.indexOf(option) < 0) || (TypeService.indexOf('FIJO POST') != -1 && arrFijoPOST.indexOf(option) < 0) || (TypeService === 'HCTNUMEROCPLANSOLOTFI' && arrHCTNumberOnlyTFI.indexOf(option) < 0)) {
                            alert('Esta transacción no está habilitada para este tipo de Plan.', strTitleMessage);
                            $('#txtCriteriaValue').val('');
                            return false;
                        }
                    }


                    return true;
                }

            }


        },
        DisabledToolbar: function (control, code, parentProduct) {
            if (parentProduct == 'PREPAID' || parentProduct == "PREPAGO") {
                if (code == "SU_SIACA_CLI" || code == "SU_SIACA_ZM") {
                    $(control).addClass("btn disabled");
                    return;
                }

            }

        },
        ValidateCode: function (code, parentProduct, title, Redirect) {
            title = ($.IsNullOrUndefinedOrEmpty(title) == false ? '' : title);
            if (title == '') {

                title = (parentProduct == 'PREPAID' ? 'PREPAGO' : (parentProduct == 'POSTPAID' ? 'POSTPAGO' : title));
            }
            if (Redirect) {
                if (Redirect == true) {
                    $.redirect.GetParamsData(code, parentProduct);
                    return;
                }
            }

            if ($.ValidateMenu(code, parentProduct)) {
                $.redirect.GetParamsData(code, parentProduct);
            }
        },
        functionValidateMenu: function (sender, e) {

            var excluir = 'SU_ACP_AIN,SU_SIACA_INST,SU_HFC_LIN,SU_ACP_IL,SU_REC_RSGA';
            var i = parseInt($(sender).data('CodeValidate'));
            var exec = false;
            Session.CO = this.id;
            Session.ORIGINAPP = this.parentProduct;

            switch (i) {
                case 1:
                    if (this.indicator != '') { exec = true; }
                    break;
                case 2:
                    if (Session.ORIGINTYPE != '') { exec = true; }
                    break;
                case 3:
                    exec = true; break;
                case 4:
                    if (excluir.indexOf(this.code) >= 0) { exec = true; }
                    break;
            }
            if (exec == true) {
                if (typeof e.fn_response !== 'undefined' && $.isFunction(e.fn_response)) {
                    e.fn_response.call(e.sender, e.control, e);
                }
                return;

            } else {

                if ($.ValidateMenu(this.code, this.parentProduct)) {
                    $.redirect.GetParamsData(this.code, this.parentProduct);
                }
            }
        }
    });

})(jQuery, null);

var string = String;

if (!string.format) {
    string.format = function (format) {
        var args = Array.prototype.slice.call(arguments, 1);
        return format.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != 'undefined'
              ? args[number]
              : match
            ;
        });
    };
}

function alert(message, title, callback) {
    $.window.open({
        autoSize: true,
        url: '',
        title: title || 'Alerta',
        text: message,
        modal: true,
        maximizeBox: false,
        minimizeBox: false,
        buttons: {
            Aceptar: {
                "class": 'btn transaction-button btn-sm',
                click: function (sender, args) {
                    this.close();
                    if (callback != null) {
                        callback.call(this);
                    }
                }
            }
        }
    });
}
function f_GetValidTipoDocumento(namecombo, nameinput, valor, e) {
    var obj = $(valor).val();// $("#txtNumeroDocumentoUsuario").val();
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 8 || tecla == 0) return true;
    var cbo = $("#" + namecombo).val();

    switch (cbo) {
        case "0": //RUC    
            $("#" + nameinput).attr('maxlength', '12');
            patron = /[1234567890]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
            break;
        case "1": //Pasaporte
            $("#" + nameinput).attr('maxlength', '15');
            patron = /[a-zA-ZÑñ1234567890@.-_áéíóú;:()+*!"·$%& ]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
            break;
        case "2": //DNI
            $("#" + nameinput).attr('maxlength', '8');
            patron = /[1234567890]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
            break;
        case "4": //Carnet Extrangeria
            $("#" + nameinput).attr('maxlength', '9');
            patron = /[1234567890]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
            break;
        case "11": //CIRE
            $("#" + nameinput).attr('maxlength', '12');
            patron = /[a-zA-ZÑñ1234567890@.-_áéíóú;:()+*!"·$%& ]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
            break;
        case "12": //CIE
            $("#" + nameinput).attr('maxlength', '12');
            patron = /[a-zA-ZÑñ1234567890@.-_áéíóú;:()+*!"·$%& ]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
            break;
        case "13": //CPP
            $("#" + nameinput).attr('maxlength', '12');
            patron = /[a-zA-ZÑñ1234567890@.-_áéíóú;:()+*!"·$%& ]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
            break;
        case "14": //CTM
            $("#" + nameinput).attr('maxlength', '12');
            patron = /[a-zA-ZÑñ1234567890@.-_áéíóú;:()+*!"·$%& ]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
            break;
        default:          
            return f_GetValidTipoDocumentoNuevo(obj, String.fromCharCode(tecla), cbo);                   
            break;
    }
}

function f_GetValidTipoDocumentoNuevo(nroDocumento, charKeyValue, sValue) {
    var sTipoDocumentos = $('#hidTipoDocumentos').val();
    if (sTipoDocumentos !== "undefined") {
        var oTipoDocumentos = sTipoDocumentos.split("|");
        for (i in oTipoDocumentos) {
            if (sValue == oTipoDocumentos[i].replace(/^.*,/, '')) {
                patron = /[0-9a-zA-Z]/;
                return patron.test(charKeyValue) && (nroDocumento.length < 12);
            }
        }
    }
    return false;
}
function f_GetValidOnlyNumber(namecombo, max, valor, e) {
        var obj = $(valor).val();
        tecla = (document.all) ? e.keyCode : e.which;
        if (tecla == 8 || tecla == 0) return true;
        var cbo = $("#" + namecombo).val();
  
    $("#" + namecombo).attr('maxlength', max);
    patron = /[1234567890]/;
    te = String.fromCharCode(tecla);
    return patron.test(te);
}
function validateRepeatNumber(valorImei) {
    var bool = false;
    var ultimodigito = valorImei.substring(valorImei.length - 1, valorImei.length)
    var regex = new RegExp("[^" + ultimodigito + "]", "g");
    valorImei = valorImei.replace(regex,"").length
    if (valorImei == 15) {
        bool = true;
    } else {
        bool = false;
    }
    return bool;
}
function confirm(message, title, callbackOk, callbackCancel, callbackClose) {
    $.window.open({
        autoSize: true,
        url: '',
        title: title || 'Confirmar',
        text: message,
        modal: true,
        controlBox: false,
        maximizeBox: false,
        minimizeBox: false,
        buttons: {
            Aceptar: {
                "class": 'btn transaction-button btn-sm',
                click: function (sender, args) {
                    this.close();
                    if (callbackOk != null) {
                        callbackOk.call(this, true);
                    }
                    if (callbackClose != null) {
                        callbackClose.call(this, true);
                    }
                }
            },
            Cancelar: {
                "class": 'btn transaction-button btn-sm',
                click: function (sender, args) {
                    this.close();
                    if (callbackCancel != null) {
                        callbackCancel.call(this, false);
                    }
                    if (callbackClose != null) {
                        callbackClose.call(this, true);
                    }
                }
            }
        }
    });
}



$(document).ajaxStop(function () {
    $.unblockUI();
});