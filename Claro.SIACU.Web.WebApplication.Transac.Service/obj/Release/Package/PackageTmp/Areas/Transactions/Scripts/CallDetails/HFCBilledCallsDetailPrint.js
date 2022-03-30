(function ($) {
    var Form = function ($element, options) {
        $.extend(this, $.fn.HFCBilledCallsDetailPrint.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            , btnCloseConstancy: $('#btnCloseConstancy', $element)
            , btnPrint: $('#btnPrintConstancy', $element)
        });
    }

    Form.prototype = {
        constructor: Form,

        init: function () {
            var that = this, controls = this.getControls();
            controls.btnCloseConstancy.addEvent(that, 'click', that.btnCloseConstancy_Click);
            controls.btnPrint.addEvent(that, 'click', that.btnPrint_Click);
            that.render();
        },

        render: function () {
            var that = this, controls = this.getControls();
            that.btnPrint_Click();
        },

        setControls: function (value) {
            this.m_controls = value;
        },

        getControls: function () {
            return this.m_controls || {};
        },

        btnCloseConstancy_Click: function () {
            parent.window.close();
        },
        btnPrint_Click: function () {
            window.print();
        }
    };

    $.fn.HFCBilledCallsDetailPrint = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('HFCBilledCallsDetailPrint'),
                options = $.extend({}, $.fn.HFCBilledCallsDetailPrint.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('HFCBilledCallsDetailPrint', data);
            }

            if (typeof option === 'string') {
                if ($.inArray(option, allowedMethods) < 0) {
                    throw "Unknown method: " + option;
                }
                value = data[option](args[1]);
            } else {
                data.init();
                if (args[1]) {
                    value = data[args[1]].apply(data, [].slice.call(args, 2));
                }
            }
        });

        return value || this;
    };

    $.fn.HFCBilledCallsDetailPrint.defaults = {
    }

    $('#divBody').HFCBilledCallsDetailPrint();
})(jQuery);
