var optCode;

(function ($, undefined) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.form.defaults, $element.data(), typeof options === 'object' && options);
        
        
        this.setControls({
            form: $element
        });

    }

    Form.prototype = {

        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

            if (this.options != null && this.options.length > 0) {
                var option, listProduct = {};
                var $divProduct, $obj;
                var t = setTimeout(function(){
                    for (var i = 0, j = that.options.length; i < j; i++) {
                        option = that.options[i];
                        if (option.descriptionAux == "POSTPAGO" || option.descriptionAux == "PREPAGO") {
                            listProduct = that.products[option.descriptionAux];

                            $obj = $('<div>', {
                                class: 'col-md-6'
                            });

                            $obj.append($('<label>', {
                                text: option.descriptionAux,
                                class: 'control-label'
                            }));

                            $divProduct = $('<div>', {
                                class: 'input-group',
                                style: 'display:block',
                                id: option.descriptionAux
                            });
                            $obj.append($divProduct);

                            for (var x = 0, y = listProduct.length; x < y; x++) {
                                that.builtProduct.call(that, $divProduct, option, listProduct[x]);
                            }
                        } else {
                            that.builtProduct.call(that, $('#POSTPAGO'), option);
                        }

                        controls.form.append($obj);
                    }
                    $("input:radio[name=optProduct]:first").attr('checked', true);
                    $("input:radio[name=optProduct]:first").trigger('click');
                    clearTimeout(t);
                }, 200)

                $('.modal-dialog').removeClass('claro-modal-xlg').addClass('claro-modal-md');
            }

            that.render();
        },
        render: function () {
            var that = this;

            
        },
        btnPopUp_click: function (send, args) {

            Session.ORIGINAPP = this.parentProduct;
            Session.CO = this.id;
            optCode = this.code;
        },

        builtProduct: function (container,option,optionProduct) {
            
            var $inputGroup, $span, $inputRadio, $inputText;
            
            $inputGroup = $('<div>', {
                class: 'input-group'
            });

            $span = $('<span>', {
                class: 'input-group-addon'
            });
            $inputGroup.append($span);

            $span.append($('<input>', {
                type: 'radio',
                name: 'optProduct',
                value: option.code
            }).addEvent(option, 'click', this.btnPopUp_click));

            $inputGroup.append($('<input>', {
                type: 'text',
                class: 'form-control',
                value: (optionProduct != null ? optionProduct : option.descriptionAux),
                readonly: true
            }));
            
            container.append($inputGroup);
        },

        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
    };

    $.fn.form = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {
            var $this = $(this),
                data = $this.data("divOptionsDefault"),
                options = $.extend({}, $.fn.form.defaults,
                    $this.data(), typeof option === 'object' && option);
            if (!data) {
                data = new Form($this, options);
                $this.data('divOptionsDefault', data);
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

      $.fn.form.defaults = {
          options: null,
          products:{
              POSTPAGO: ['Móvil', 'Fija', 'TFI', 'TPI', 'BAM'],
              PREPAGO: ['Móvil', 'Fija']
          }
      }

})(jQuery);

$(document).ready(function () {
    $('#divOptionsDefault').form({ options: btnDynamic });
});
