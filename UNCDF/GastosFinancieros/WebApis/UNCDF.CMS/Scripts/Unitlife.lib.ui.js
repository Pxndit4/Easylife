/// <reference path="jquery-2.2.2.js" />
/// <reference path="jquery.validate.js" />

(function ($) {

    $.fn.busy = function (action) {
        var elm = null;
        if (!this.hasClass('busy-btn')) {
            elm = this.find('.busy-btn');
            if (elm.length == 0) {
                this.append(
                '<div class="busy-btn busy-center" style="color:#fff;">' +
                    '<span class="glyphicon glyphicon-refresh glyphicon-refresh-animate"></span>' +
                    '<span class="busy-text">Cargando ...</span>' +
                '</div>');
            }

            elm = this.find('.busy-btn');
        }
        else
            elm = this;

        if (action === "open" || action == "show") {
            elm.show();
        }

        if (action === "close" || action == "hide") {
            elm.hide();
        }
    };

    $.fn.serializeObject2 = function () {
        var o = {};
        var fields = $("input, textarea, select", $(this));
        $(fields).each(function (i, element) {
            var _d = $(element).data('isboolean') || '';
            var _val = $(element).val();

            if (_d != '') {
                if (_d == true || _d == false) {
                    _val = (_val == "1" | _val == "true" ? true : false);
                }
            }

            o[$(element).attr("name")] = _val;

            //if (o[$(element).attr("name")] !== undefined) {
            //    //var arr = o[$(element).attr("name")];
            //    //if (Array.isArray(arr) == false) {
            //    //    o[$(element).attr("name")] = [arr];
            //    //}
            //    //arr = o[$(element).attr("name")];
            //    //arr.push($(element).val());
            //} else {
            //    o[$(element).attr("name")] = $(element).val();
            //}
        });

        return o;
    };

    $.fn.serializeObjectDom = function (groupContent) {
        var o = [];
        $(this.find(groupContent)).each(function (i, e) {
            var fields = $("input, textarea, select", $(e));
            o.push($(e).serializeObject2())
        });
        return o;
    };

    $.fn.checked = function (value) {
        if (value != undefined) {
            this[0].checked = (value == true || value == 1);

            return;
        }
        if (this.length == 0 || this.get(0).type != "checkbox") return false;
        return this.is(":checked");
    };


    $.fn.setDisable = function (value) {
        if (value == true) {
            this.attr("disabled", "disabled");
        }
        else if (value == false)
            this.removeAttr("disabled");
    };


}(jQuery));


var unitlife = {
    perfil: {
        elements: {
            topPerfil: "#topPerfil",
            userPerfil: "#userPerfil",
            perfilGroupItem: "#userPerfil .list-group-item",
            mainlayout: "#mainlayout"
        },
        onChangeAtach: function () {
            $(unitlife.perfil.elements.perfilGroupItem).click(function () {
                var _this = $(this);
                if (_this.hasClass('active'))
                    return false;

                $(unitlife.perfil.elements.perfilGroupItem).removeClass("active");
                _this.addClass("active");

                $(unitlife.perfil.elements.topPerfil).text(_this.text());

                if (unitlife.perfil.getCurrent() != this.id) {
                    $(unitlife.perfil.elements.mainlayout).html('');
                }
            });
        },
        getCurrent: function () {
            return $("#__targetPrfl").val();
        },
        onBegin: function (e, s) {
            $("#navbarBusy").busy('open');
        },
        onComplete: function (e, s) {
            $("#navbarBusy").busy('close');
            $("#startModal").modal('hide');

            $("#panelContent").find(".collapse").first().collapse('show');
        }
    },
    empresa: {
        elements: {
            topEmpresa: "#topEmpresa",
            userEmpresa: "#userEmpresa",
            empresaGroupItem: "#userEmpresa .list-group-item",
            mainlayout: "#mainlayout"
        },
        onChangeAtach: function () {
            $(unitlife.empresa.elements.empresaGroupItem).click(function () {
                var _this = $(this);
                if (_this.hasClass('active'))
                    return false;

                $(unitlife.empresa.elements.empresaGroupItem).removeClass("active");
                _this.addClass("active");

                $(unitlife.empresa.elements.topEmpresa).text(_this.text());

                if (unitlife.empresa.getCurrent() != this.id) {
                    $(unitlife.empresa.elements.mainlayout).html('');
                }
                $('#tabnavbar').empty();

            });
        },
        getCurrent: function () {
            return $("#__targetPrfl").val();
        },
        onBegin: function (e, s) {
            $("#navbarBusy").busy('open');
        },
        onComplete: function (e, s) {
            $("#navbarBusy").busy('close');
            $("#startModal").modal('hide');

            $("#panelContent").find(".collapse").first().collapse('show');
        }
    },
    site: {
        initialize: function () {
            this.navBar.init();
        },
        siteValidator: function () {

        },
        content: {
            getElement: function () {
                return $('.sys-content-layout');
            },
            togle: function () {
                this.getElement().addClass('layout-togled');
            },
            unTogle: function () {
                this.getElement().removeClass('layout-togled');
            }
        },
        navBar: {
            init: function () {
                this.getElement().find('.sys-menu-top-ico').bind("click", function (e) {
                    var _nav = unitlife.site.navBar;
                    unitlife.site.navBar.togle();
                    return false;
                });

                this.getElement().find('.sys-menu-top-ico-hamb').bind("click", function (e) {
                    unitlife.site.navBar.unTogle();
                    return false;
                });

            },
            itemsOn: function () {
                var _target = $("#__target");
                if (_target.val() == '')
                    _target.val('0');
                var _mnu = this.getElement().find("#" + _target.val());

                _mnu.parent().addClass("active");
                _mnu.closest('.panel-collapse').collapse('show');

                if (_mnu.length == 0)
                    this.unTogle();
                else
                    this.togle();

                var annn = this.getElement().find('#tabnavbar').find('.panel-item a');
                annn.click(function () {
                    unitlife.site.navBar.getElement().find('.active').removeClass('active');
                    unitlife.site.navBar.getElement().find("#" + this.id).parent().addClass("active");
                    unitlife.site.navBar.togle();
                    unitlife.site.busyMain.show();

                });
            },
            getElement: function () {
                return $(".sys-nav");
            },
            isTogled: function () {
                return this.getElement().hasClass('nav-togled');
            },
            togle: function () {
                if (this.isTogled())
                    return;
                this.getElement().addClass("nav-togled");
                var da = this.getElement().find('.sys-menu-top-ico').find('.glyphicon');
                da.removeClass('glyphicon-circle-arrow-left');
                da.addClass('glyphicon-menu-hamburger');

                unitlife.site.content.togle();
            },
            unTogle: function () {
                this.getElement().removeClass("nav-togled");
                var da = this.getElement().find('.sys-menu-top-ico').find('.glyphicon');
                da.removeClass('glyphicon-menu-hamburger');
                da.addClass('glyphicon-circle-arrow-left');
                unitlife.site.content.unTogle();
            }
        },
        busyMain: {
            hide: function () {
                $("#startModal").modal('hide');
            },
            show: function () {
                $("#startModal").modal('show');
            }
        },
        dialog: {
            onInit: function (e, s) {
                var _this = unitlife.site.dialog;
                _this.emptyBody();

                _this.show();
                $("#dialogMain").find('#dialogBusy').busy('open');
            },
            onInitCustom: function (data) {
                // var width = data.widthPopup || 0;
                //if (width > 0)
                //    $("#dialogMain div:first-child").css("width", width.toString());

                var _this = unitlife.site.dialog;
                _this.emptyBody();

                _this.show();
                $("#dialogMain").find('#dialogBusy').busy('open');
            },
            onComplete: function (e, s) {
                $("#dialogMain").find('#dialogBusy').busy('close');
                var _dial = $("#dialogMain");
                var txt = _dial.find('#dialogBody').find('#viewTitle').val();
                if (txt != null && txt != '')
                    _dial.find('.modal-header').find('.modal-title').text(txt);
                else
                    _dial.find('.modal-header').find('.modal-title').text('Sistema');

            },
            onFailure: function (e, s) {
                unitlife.site.dialog.close();
                unitlife.ui.MessagePanel.show({ message: 'Ocurrió error en la operación', isError: true });
            },
            isOpen: function () {
                return $("#dialogMain").css("display") == 'block';
            },
            show: function () {
                $("#dialogMain").modal('show');
            },
            emptyBody: function () {
                $("#dialogMain").find('#dialogBody').empty();
            },
            getBodyElement: function () {
                return $("#dialogMain").find('#dialogBody');
            },
            close: function (v) {
                var dBdy = $("#dialogMain").find('#dialogBody');

                if (dBdy.find('#errViewEx').length > 0) {
                    var a = dBdy.find('#errViewEx').find('#errViewExMsg').text();
                    if (a != '') {
                        unitlife.ui.MessagePanel.show({ message: a, isError: true });
                    }
                }
                $("#dialogMain").modal('hide');

                $("#dialogMain").find('#targetValue').val("{}");

            },
            busy:
            {
                show: function () {
                    return $("#dialogMain").find('#dialogBusy').busy('show');
                },
                hide: function () {
                    return $("#dialogMain").find('#dialogBusy').busy('hide');
                }

            },
            invoqResultCallback: function (key, data) {
                if (___getModalResult && typeof ___getModalResult == "function") {
                    ___getModalResult.call(this, key, { item: data });
                }
            },
            setTargetValue: function (value) {
                $("#dialogMain").find('#targetValue').val(typeof value == "object" ? JSON.stringify(value) : value);
            },
            getTargetValue: function () {
                return $("#dialogMain").find('#targetValue').val() || '';
            }
        }
    },
    ajax: {
        load: function (options) {
            options = $.extend(
                {},
                {
                    form: null,
                    data: null,
                    url: "",
                    beforeSend: function () { },
                    success: function () { },
                    error: function () { },
                    complete: function () { },
                    contentType: "application/json",
                    dataType: "json",
                    type: 'POST'
                },
                options || {});

            var data = (options.form != null ? { model: $(options.form).serializeObject2() } : options.data);

            $.ajax({
                type: options.type,
                url: options.url,
                data: JSON.stringify(data),
                contentType: options.contentType,
                dataType: options.dataType,
                beforeSend: options.beforeSend,
                success: options.success,
                error: function (request, status, error) {
                    if (request.status == 420) { var response = $.parseJSON(request.responseText); window.location = response.LogOutUrl; } else { if (options.error) { options.error(); } }
                },
                complete: options.complete
            });
        },
        getParameters: function (url, onSucc, onError) {

            $.ajax({

                url: url,
                type: 'POST',
                success: onSucc
            });
            
        },
        getJson: function (url, onSucc, onError) {
            var _this = unitlife.ajax;
            _this.load({
                url: url,
                success: onSucc,
                error: onError
            });
        },
        submit: function (url, dataOrFormName, onSuccess, onError) {
            var _this = unitlife.ajax;

            unitlife.site.dialog.busy.show();
            _this.load({
                url: url,
                form: (typeof dataOrFormName == "string" ? dataOrFormName : null),
                data: (typeof dataOrFormName == "string" ? null : dataOrFormName),
                success: function (e, s) {
                    if (typeof onSuccess == "function") {
                        if (onSuccess.call) {
                            onSuccess.call(this, e, s);
                        }
                    }
                    if (e.isError == false) {
                        setTimeout(function (e) {
                            unitlife.site.dialog.close();
                        }, 1000);
                    }
                    unitlife.site.dialog.busy.hide();
                    unitlife.ui.MessagePanel.show(e);
                },
                error: function (e, s) {
                    if (typeof onError == "function") {
                        if (onError.call) {
                            onError.call(this, e, s);
                        }
                    }
                    unitlife.ui.MessagePanel.show({ message: 'Ocurrió error en la operación.', isError: true });
                    unitlife.site.dialog.close();
                    unitlife.site.dialog.busy.hide();
                }
            });
        },
        submitNotClose: function (url, dataOrFormName, onSuccess, onError) {
            var _this = unitlife.ajax;

            unitlife.site.dialog.busy.show();
            _this.load({
                url: url,
                form: (typeof dataOrFormName == "string" ? dataOrFormName : null),
                data: (typeof dataOrFormName == "string" ? null : dataOrFormName),
                success: function (e, s) {
                    if (typeof onSuccess == "function") {
                        if (onSuccess.call) {
                            onSuccess.call(this, e, s);
                        }
                    }
                    if (e.isError == false) {
                        setTimeout(function (e) {
                           /// unitlife.site.dialog.close();
                        }, 1000);
                    }
                    unitlife.site.dialog.busy.hide();
                    unitlife.ui.MessagePanel.show(e);
                },
                error: function (e, s) {
                    if (typeof onError == "function") {
                        if (onError.call) {
                            onError.call(this, e, s);
                        }
                    }
                    unitlife.ui.MessagePanel.show({ message: 'Ocurrió error en la operación.', isError: true });
                    unitlife.site.dialog.close();
                    unitlife.site.dialog.busy.hide();
                }
            });
        },
        submitAddFiles: function (url, data, onSuccess, onError) {
            var _this = unitlife.ajax;

            unitlife.site.dialog.busy.show();

            $.ajax({
                url: url,
                type: "POST",
                processData: false,
                contentType: false,
                data: data,
                success: function (e, s) {
                    if (typeof onSuccess == "function") {
                        if (onSuccess.call) {
                            onSuccess.call(this, e, s);
                        }
                    }
                    if (e.isError == false) {
                        setTimeout(function (e) {
                             unitlife.site.dialog.close();
                        }, 1000);
                    }
                    unitlife.site.dialog.busy.hide();
                    unitlife.ui.MessagePanel.show(e);
                },
                error: function (e, s) {
                    if (typeof onError == "function") {
                        if (onError.call) {
                            onError.call(this, e, s);
                        }
                    }
                    unitlife.ui.MessagePanel.show({ message: 'Ocurrió error en la operación.', isError: true });
                    unitlife.site.dialog.close();
                    unitlife.site.dialog.busy.hide();
                }

            });


            //_this.load({
            //    url: url,
            //    //form: (typeof dataOrFormName == "string" ? dataOrFormName : null),
            //    data: dataOrFormName,
            //    success: function (e, s) {
            //        if (typeof onSuccess == "function") {
            //            if (onSuccess.call) {
            //                onSuccess.call(this, e, s);
            //            }
            //        }
            //        if (e.isError == false) {
            //            setTimeout(function (e) {
            //                unitlife.site.dialog.close();
            //            }, 1000);
            //        }
            //        unitlife.site.dialog.busy.hide();
            //        unitlife.ui.MessagePanel.show(e);
            //    },
            //    error: function (e, s) {
            //        if (typeof onError == "function") {
            //            if (onError.call) {
            //                onError.call(this, e, s);
            //            }
            //        }
            //        unitlife.ui.MessagePanel.show({ message: 'Ocurrió error en la operación.', isError: true });
            //        unitlife.site.dialog.close();
            //        unitlife.site.dialog.busy.hide();
            //    }
            //});
        },
        submitAddFilesNotClose: function (url, data, onSuccess, onError) {
            var _this = unitlife.ajax;

            unitlife.site.dialog.busy.show();
            

            $.ajax({
                url: url,
                type: "POST",
                processData: false,
                contentType: false,
                data: data,
                success: function (e, s) {
                    if (typeof onSuccess == "function") {
                        if (onSuccess.call) {
                            onSuccess.call(this, e, s);
                        }
                    }
                    if (e.isError == false) {
                        //setTimeout(function (e) {
                        //    unitlife.site.dialog.close();
                        //}, 1000);
                    }
                    //unitlife.site.dialog.busy.hide();
                    //unitlife.ui.MessagePanel.show(e);
                },
                error: function (e, s) {
                    if (typeof onError == "function") {
                        if (onError.call) {
                            onError.call(this, e, s);
                        }
                    }
                    unitlife.ui.MessagePanel.show({ message: 'Ocurrió error en la operación.', isError: true });
                    //unitlife.site.dialog.close();
                    //unitlife.site.dialog.busy.hide();
                }

            });
        },

        getView: function (appendTo, url, data, onSuccess, onError) {
            var _this = unitlife.ajax;
            unitlife.site.dialog.busy.show();

            $.get(url, data, function (e, s) {
                $(appendTo).html(e);
                if (typeof onSuccess == "function") {
                    if (onSuccess.call) {
                        onSuccess.call(this, e, s);
                    }
                }
                unitlife.site.dialog.busy.hide();
                unitlife.site.dialog.close();
                unitlife.ui.MessagePanel.show(e);

            }).error(function (e, s) {
                if (typeof onError == "function") {
                    if (onError.call) {
                        onError.call(this, e, s);
                    }
                }
                unitlife.ui.MessagePanel.show({ message: 'Ocurrió error en la operación.', isError: true });
                unitlife.site.dialog.close();
                unitlife.site.dialog.busy.hide();
            });


        }
    },
    ui: {
        SelectedValue: function (ddl, value) {
            var _val = value, _this = unitlife.ui;
            if (_val == undefined || _val == null) {
                _val = $(ddl).data('value');
                if (_val == '')
                    return;
            }

            $(ddl).val(_val);
        },
        DropDownListFill: function (ddl, data, addSelItem) {

            if (addSelItem == true || addSelItem == 'S') {
                $(document.createElement('option'))
                  .attr('value', '')
                  .text('Seleccione')
                  .appendTo(ddl);
            }
            else if (addSelItem == 'T') {
                $(document.createElement('option'))
                 .attr('value', '')
                 .text('Todos')
                 .appendTo(ddl);
            }

            $(data).each(function (i, e) {
               
                     $(document.createElement('option'))
                  .attr('value', e.Value)
                  .text(e.Text) 
                  .appendTo(ddl);
               
               
            });
        },
        DropDownListSelectedFill: function (ddl, data, addSelItem,selected) {

            if (addSelItem == true || addSelItem == 'S') {
                $(document.createElement('option'))
                  .attr('value', '')
                  .text('Seleccione')
                  .appendTo(ddl);
            }
            else if (addSelItem == 'T') {
                $(document.createElement('option'))
                 .attr('value', '')
                 .text('Todos')
                 .appendTo(ddl);
            }

            $(data).each(function (i, e) {  
                if (e.Value == selected ) {
                    $(document.createElement('option'))
                 .attr('value', e.Value)
                 .text(e.Text)
                 .attr('selected', 'selected')
                 .appendTo(ddl);
                } else {
                    $(document.createElement('option'))
               .attr('value', e.Value)
               .text(e.Text)
               .appendTo(ddl);
                }

            });
        },
        DropDownListFillJson: function (url, ddl, onError, addSelItem) {
            var _ajax = unitlife.ajax;
            var _this = unitlife.ui;
            var onSuccCallback = function (e) {

                _this.DropDownListFill(ddl, e.data || [], addSelItem);

                var dl = $(ddl), _val = '';
                _this.SelectedValue(ddl, null);
                _val = dl.val();
                if (_val == null || _val == '')
                    dl.val('');
                else
                    dl.change();
            };

            _ajax.getJson(url, onSuccCallback, onError);
        },
        DropDownListFillSelectedJson: function (url, ddl, onError, addSelItem,select) {
            var _ajax = unitlife.ajax;
            var _this = unitlife.ui;
            var onSuccCallback = function (e) {

                _this.DropDownListSelectedFill(ddl, e.data || [], addSelItem, select);

                var dl = $(ddl), _val = '';
                _this.SelectedValue(ddl, null);
                _val = dl.val();
                if (_val == null || _val == '')
                    dl.val('');
                else
                    dl.change();
                //ddl.value = "0000";
            };

            _ajax.getJson(url, onSuccCallback, onError);
        },
        EmptyDropDownList: function (ddl) {
            $(ddl).children().remove();
        },
        DropDownListFillJsonError: function (e, s) {
        },
        MessagePanel: {
            init: function (elm) {
                var _elm = $(elm);
                if (_elm.find('#content').length == 0)
                    $(document.createElement('span')).attr("id", "content").addClass('content').appendTo(_elm);
                if (_elm.find('#ahid').length == 0) {
                    $('<a style="float:right" data-toggle="collapse" aria-expanded="false" id="ahid" href="#panMessage"><span class="glyphicon glyphicon-remove"></span></a>').appendTo(_elm);
                }
                _elm.find('#ahid').unbind("click");
                _elm.find('#ahid').bind("click", function (e) {
                    $(this).closest('.ui-error-view').collapse('hide');
                });

                _elm.on('hidden.bs.collapse', function (e) {
                    var _t = $(this).data("target");
                    if (_t == undefined || _t == '')
                        return;
                    clearTimeout(_t);
                })

                return _elm;
            },
            show: function (data, autoclose) {
                if (typeof data == "string")
                    return;
                autoclose = autoclose || true;
                var cntPn = $('.sys-content .ui-error-view');
                if (unitlife.site.dialog.isOpen()) {
                    var dialBody = unitlife.site.dialog.getBodyElement();
                    cntPn = dialBody.find('.ui-error-view');
                    if (cntPn.length == 0) {
                        var _identElement = dialBody.find('.ui-bar');
                        if (_identElement.length != 0)
                            _identElement.after('<div class="ui-error-view collapse" id="panMessage" ></div>');
                        else {
                            _identElement = dialBody.children().first();
                            _identElement.before('<div class="ui-error-view collapse" id="panMessage" ></div>');
                        }
                    }
                    cntPn = dialBody.find('.ui-error-view');
                }
                cntPn = this.init(cntPn);
                cntPn.find('#content').text(data.message || '');
                cntPn.removeClass('viewerror');
                cntPn.removeClass('error');
                cntPn.removeClass('invalid');
                cntPn.removeClass('ok');
                cntPn.removeClass('warning');

                if (data.isError == true) {
                    cntPn.addClass("error");
                }
                else if (data.type && data.type != '') {
                    cntPn.addClass(data.type);
                }
                else
                    cntPn.addClass("ok");

                cntPn.collapse();
                cntPn.collapse('show');

                if (autoclose == true) {
                    var _this = this;
                    var _data = {
                        id: "#" + (cntPn.parent().attr("id") || '') + ' #' + cntPn.attr("id"), panel: cntPn
                    };

                    var _trgTime = setTimeout(function (v) {
                        if (v == undefined)
                            v = _data;
                        if (v.panel)
                            v.panel.collapse('hide');
                    }, 20000, _data);
                    cntPn.data("target", _trgTime);
                }
            },
            hide: function () {
                if (unitlife.site.dialog.isOpen()) {
                    unitlife.site.dialog.getBodyElement().find('.ui-error-view').collapse('hide');
                }
                else
                    $('.ui-error-view').collapse('hide');
            }
        },
        validation: function (frmElm) {
            var _isValid = [];
            var _isMulti = false;

            var _res = {
                unobtrusiveParse: function () {
                    jQuery.validator.unobtrusive.parse();
                    jQuery.validator.unobtrusive.parse(frmElm);
                },
                $form: function () {
                    return $(frmElm);
                },
                showMessage: function () {
                    unitlife.ui.validation.showMessage();
                },
                getIsValid: function () {
                    return _isValid;
                },
                setMultiple: function (value) {
                    _isMulti = value;
                },
                validMultiple: function () {
                    var r = true;
                    $(_isValid).each(function (i, e) {
                        if (e == false) {
                            r = false;
                            return false;
                        }
                    });

                    if (r == false)
                        unitlife.ui.validation.showMessage();
                    else
                        unitlife.ui.MessagePanel.hide();

                    return r;
                },
                validGrid: function (grid) {
                    var gs = unitlife.ui.grid(grid);
                    var _result = false;
                    if (gs.getData().length == 0) {
                        var _cont = gs.gridElement().closest(".fixed-table-container");
                        _cont.addClass("input-validation-error");

                        _result = false;
                    }
                    else {
                        gs.gridElement().closest(".fixed-table-container").removeClass("input-validation-error");
                        _result = true;
                    }
                    if (!_isMulti) {
                        if (_result == false)
                            unitlife.ui.validation.showMessage();
                        else
                            unitlife.ui.MessagePanel.hide();
                    }

                    if (_isMulti)
                        _isValid.push(_result);

                    return _result;
                },
                valid: function (element) {
                    unitlife.ui.form(frmElm).checkInputs();
                    var _valid = false;

                    if ($.isArray(element)) {

                        var _f = this.$form(), _result = true, _fc = false;
                        $(element).each(function (i, e) {
                            var v = _f.find(e).valid();
                            if (v == false) {
                                _result = false
                            }
                        });

                        if (_result == false)
                            this.showMessage("Campos obligatorios.");
                        else
                            unitlife.ui.MessagePanel.hide();

                        return _result;
                    }

                    if (element == undefined || element == null) {
                        _valid = this.$form().valid();
                        this.$form().find('.input-validation-error').first().focus();
                    }
                    else
                        _valid = this.$form().find(element).valid();
                    if (!_isMulti) {
                        if (_valid == false)
                            unitlife.ui.validation.showMessage();
                        else
                            unitlife.ui.MessagePanel.hide();
                    }
                    if (_isMulti)
                        _isValid.push(_valid);
                    return _valid;
                },
                validBloques: function (elementosHtml) {
                    /*var conten = $("#contenedor02");
                    var _controls = $("input[type=text], textarea, select", conten);
                    _controls.each(function (i, e) {
                        if ($(e).attr("data-val") == "true"){
                            e.value = unitlife.ui.string(e.value).trim();
                            e.value = unitlife.ui.string(e.value).mtrim();
                        }
                    });*/

                    // var elementosHtml = "#contenedor02";
                    // var elementosHtml = ["#contenedor02", "#contenedor03"]

                    // Proceso de validacion de campos marcados con data-val="true"
                    // Valida los INPUT de una o mas secciones de HTML, marca los controles
                    // que no tienen contenido con un estilo de error y retorna un resultado
                    // boleano de la evaluacion total de los controles
                    var listaElementos = $.isArray(elementosHtml) ? elementosHtml : [elementosHtml];
                    var resultadoValidacion = true;

                    for (var i = 0; i < listaElementos.length; i++) {
                        var _controls = $(listaElementos[i]).find('[data-val="true"]');

                        _controls.each(function (i, e) {
                            // Primero hacemos trim para omitir los espacios
                            e.value = unitlife.ui.string(e.value).trim();
                            e.value = unitlife.ui.string(e.value).mtrim();

                            // Por defecto retiramos la marca de ERROR    
                            $(e).removeClass("input-validation-error");

                            if (e.value.length == "") {
                                // Si el control no tiene contenido le aplicamos la marca de ERROR
                                $(e).addClass("input-validation-error");
                                resultadoValidacion = false;
                            }
                        });
                    }

                    if (resultadoValidacion)
                        unitlife.ui.MessagePanel.hide();
                    else
                        unitlife.ui.validation.showMessage();

                    return resultadoValidacion;
                },

                notificarErrorElemento: function (elemento) {
                    if (!$(elemento).hasClass("input-validation-error"))
                        $(elemento).addClass("input-validation-error");
                    var msg = $(elemento).attr("data-val-required");
                    unitlife.ui.validation.showMessage(msg);
                },

                validGenericSearcher: function (controls) {
                    unitlife.ui.form(frmElm).checkInputs();
                    if (controls == undefined || controls == null) {
                        controls = this.$form().find('[data-val="true"]');
                    }

                    var _f = this.$form(), _result = true, _fc = false;
                    var _vald = false;
                    this.unobtrusiveParse();
                    var _valTrue = 0;
                    var _valFalse = 0;
                    var _emptTrue = 0;
                    $(controls).each(function (i, e) {
                        var _a = $(e).is("input") ? $(e) : _f.find(e);

                        var v = false;
                        if (_a.val() == '') {
                            v = true;
                            _emptTrue++;
                        }
                        else
                            v = _a.valid();
                        if (v == false) {
                            _result = false;
                            _valFalse++;
                        }
                        else
                            _valTrue++;
                    });

                    if (_valTrue == _emptTrue) {
                        this.showMessage("Campos obligatorios.");
                        _valTrue = 0;
                        $(controls).each(function (i, e) {
                            var _a = $(e).is("input") ? $(e) : _f.find(e);
                            _a.valid();
                        });
                    }
                    else if (_valTrue == 0 || (_valTrue > 0 && _valFalse > 0)) {
                        this.showMessage("Campos obligatorios.");
                        _valTrue = 0;
                    }
                    else {
                        this.resetForm();
                        unitlife.ui.MessagePanel.hide();
                    }

                    return _valTrue > 0;
                },
                validGroup: function (val1, val2, controls) {
                    unitlife.ui.form(frmElm).checkInputs();
                    var _f = this.$form(), _result = true, _fc = false;
                    $(controls).each(function (i, e) {
                        var v = _f.find(e).valid();
                        if (v == false) {
                            if (_fc == false) {
                                _f.find(e).focus();
                                _fc = true;
                            }
                            _result = false;
                        }
                    });
                    if (!_isMulti) {
                        if (_result == false)
                            this.showMessage("Campos obligatorios.");
                        else
                            unitlife.ui.MessagePanel.hide();
                    }
                    _isValid = _result;
                    if (_isMulti)
                        _isValid.push(_result);
                    return _result;
                },
                resetForm: function () {
                    unitlife.ui.form(frmElm).checkInputs();
                    var v = this.$form().validate();
                    if (v)
                        v.resetForm();
                },
                validByRegx: function () {
                    unitlife.ui.form(frmElm).checkInputs();
                    var _valTrue = true;
                    var _isValid = true;
                    var controls = this.$form().find('[data-val="true"]');
                    controls.each(function (i, e) {
                        var _dataRegx = $(e).data('val-regex-pattern');
                        var _val = $(e).val();
                        var _elm = $(e);
                        if (_val != '' && _dataRegx != undefined) {
                            var gx = new RegExp(_dataRegx)
                            if (gx.test(_val) == false) {
                                _elm.addClass('input-validation-error');
                                _isValid = false;
                            }
                            else
                                _elm.removeClass('input-validation-error');
                        }
                    });

                    this.$form().find('.input-validation-error').first().focus();

                    if (_isValid == false)
                        unitlife.ui.validation.showMessage();
                    else
                        unitlife.ui.MessagePanel.hide();

                    return _isValid;
                }
            };
            return _res;
        },
        grid: function (element) {
            var _onPostResetLayout = null;

            var _int = {
                busy: function s() {

                }
            };
            var _result = {
                // Permite hacer llamada a una funcion una vez terminado el método ResetLayout
                setOnPostResetLayout: function (callback) {
                    _onPostResetLayout = callback;
                },
                gridElement: function () {
                    return $(element);
                },
                gridContent: function () {
                    return _result.gridElement().closest('.bootstrap-table');
                },
                create: function (options) {
                    options = $.extend({
                        cache: false,
                        search: false,
                        pagination: false,
                        footerIntell: true,
                        //paginationSuccessivelySize: 0,
                        //paginationPagesBySide: 0,
                        //paginationUseIntermediate: true,
                        //height:480,
                        rowStyle: 'unitlife.ui.grid.rowStateGenericStyle'
                    }, options || {});

                    var _bootgrid = this.gridElement();
                    _bootgrid.bootstrapTable(options);

                    _bootgrid.on("post-body.bs.table	", function (e) {
                        $(this).bootstrapTable('resetView');
                    });

                    this.clearNoFoundRows();

                    if (options.footerIntell == true) {
                        this.hideFooter();
                    }
                    this.resetLayout();

                    _bootgrid.addClass("ui-grid");
                },
                clearNoFoundRows: function () {
                    this.gridElement().find('.no-records-found').find('td').html('&nbsp;');
                },
                showDataInfo: function () {
                    var _cont = this.gridElement().closest(".ui-container");
                    var da = $(".ui-grid-rows[data-info-grid='" + element + "']");
                    var _dtmp = da.data("info");
                    if (_dtmp == undefined)
                        return;
                    _dtmp = _dtmp.replace("{0}", this.getData().length);
                    da.html(_dtmp);
                },
                load: function (url, formNameOrData, onSuccCallback, onFailtCallback/*, onPostLoad*/) {
                    _result.gridContent().busy('show');

                    unitlife.ajax.load(
                            {
                                url: url,
                                form: (typeof formNameOrData == "string" ? formNameOrData : null),
                                data: (typeof formNameOrData == "string" ? null : formNameOrData),
                                success: function (data, s) {
                                    if (data && data.message != null || data.isError == true) {
                                        unitlife.ui.MessagePanel.show(data);
                                        setTimeout(function (a) {
                                            if (a == undefined) {
                                                a = data;
                                            }

                                            unitlife.ui.MessagePanel.show(a);
                                        }, 300, data);
                                    }
                                    else {
                                        _result.gridElement().bootstrapTable('load', data);
                                        _result.showDataInfo();
                                        if (_result.getData().length == 0 && _result.isFootter())
                                            _result.hideFooter();
                                        else if (_result.getData().length > 0 && _result.isFootter())
                                            _result.showFooter();
                                    }

                                    if (typeof onSuccCallback == "function") {
                                        if (onSuccCallback.call) {
                                            onSuccCallback.call(this, data, s);
                                        }
                                    }

                                    _result.gridContent().busy('close');

                                    _result.resetLayout(/*onPostLoad*/);
                                },
                                complete: function (e, s) {
                                    _result.gridContent().busy('close');
                                },
                                error: function (e, s) {
                                    unitlife.ui.MessagePanel.show({ message: 'Ocurrió error en la operación.', isError: true });
                                    if (typeof onFailtCallback == "function") {
                                        if (onFailtCallback.call) {
                                            onFailtCallback.call(this, e, s);
                                        }
                                    }
                                    _result.gridContent().busy('close');
                                }
                            }
                        );
                },
                loadFile: function (url, data, onSuccCallback, onFailtCallback/*, onPostLoad*/) {
                    _result.gridContent().busy('show');

                    $.ajax({
                        url: url,
                        type: "POST",
                        processData: false,
                        contentType: false,
                        data: data,
                        //form: form,
                        success: function (data, s) {

                            if (data && data.message != null || data.isError == true) {
                                unitlife.ui.MessagePanel.show(data);
                                setTimeout(function (a) {
                                    if (a == undefined) {
                                        a = data;
                                    }

                                    unitlife.ui.MessagePanel.show(a);
                                }, 300, data);
                            }
                            else {
                                _result.gridElement().bootstrapTable('load', data);
                                _result.showDataInfo();
                                if (_result.getData().length == 0 && _result.isFootter())
                                    _result.hideFooter();
                                else if (_result.getData().length > 0 && _result.isFootter())
                                    _result.showFooter();
                            }

                            if (typeof onSuccCallback == "function") {
                                if (onSuccCallback.call) {
                                    onSuccCallback.call(this, data, s);
                                }
                            }

                            _result.gridContent().busy('close');

                            _result.resetLayout(/*onPostLoad*/);

                        },
                        complete: function (e, s) {
                            _result.gridContent().busy('close');
                        },
                        error: function (e, s) {
                            unitlife.ui.MessagePanel.show({ message: 'Ocurrió error en la operación.', isError: true });
                            if (typeof onFailtCallback == "function") {
                                if (onFailtCallback.call) {
                                    onFailtCallback.call(this, e, s);
                                }
                            }
                            _result.gridContent().busy('close');
                        }

                    });

                    
                    //unitlife.ajax.load(
                    //    {

                    //        url: url,
                    //        form: (typeof formNameOrData == "string" ? formNameOrData : null),
                    //        data: (typeof formNameOrData == "string" ? null : formNameOrData),
                    //        success: function (data, s) {
                    //            if (data && data.message != null || data.isError == true) {
                    //                unitlife.ui.MessagePanel.show(data);
                    //                setTimeout(function (a) {
                    //                    if (a == undefined) {
                    //                        a = data;
                    //                    }

                    //                    unitlife.ui.MessagePanel.show(a);
                    //                }, 300, data);
                    //            }
                    //            else {
                    //                _result.gridElement().bootstrapTable('load', data);
                    //                _result.showDataInfo();
                    //                if (_result.getData().length == 0 && _result.isFootter())
                    //                    _result.hideFooter();
                    //                else if (_result.getData().length > 0 && _result.isFootter())
                    //                    _result.showFooter();
                    //            }

                    //            if (typeof onSuccCallback == "function") {
                    //                if (onSuccCallback.call) {
                    //                    onSuccCallback.call(this, data, s);
                    //                }
                    //            }

                    //            _result.gridContent().busy('close');

                    //            _result.resetLayout(/*onPostLoad*/);
                    //        },
                    //        complete: function (e, s) {
                    //            _result.gridContent().busy('close');
                    //        },
                    //        error: function (e, s) {
                    //            unitlife.ui.MessagePanel.show({ message: 'Ocurrió error en la operación.', isError: true });
                    //            if (typeof onFailtCallback == "function") {
                    //                if (onFailtCallback.call) {
                    //                    onFailtCallback.call(this, e, s);
                    //                }
                    //            }
                    //            _result.gridContent().busy('close');
                    //        }
                    //    }
                    //);
                },
                resetLayout: function () {
                    var _x = 0;
                    _result.gridElement().bootstrapTable('resetView');

                    //if(typeof _onPostResetLayout == "function") {
                    //    if (_onPostResetLayout.call) {
                    //        _onPostResetLayout.call();
                    //        return;
                    //    }                        
                    //}

                    setTimeout(function () {
                        _result.gridElement().bootstrapTable('resetView');
                    }, 450);

                    var _pt = setInterval(function () {
                        _result.gridElement().bootstrapTable('resetView');
                        if (_x >= 2)
                            clearInterval(_pt);
                        _x++;

                        if(typeof _onPostResetLayout == "function") {
                            if(_onPostResetLayout.call) {
                                _onPostResetLayout.call();
                                return;
                            }
                        }

                    }, 1000);
                },
                loadData: function (data) {
                    _result.gridContent().busy('show');
                    _result.gridElement().bootstrapTable('load', data);
                    _result.gridContent().busy('close');
                    if (_result.getData().length == 0 && _result.isFootter())
                        _result.hideFooter();
                    else if (_result.getData().length > 0 && _result.isFootter())
                        _result.showFooter();
                    this.resetLayout();
                },
                invoqCommand: function (url, data, onSuccCallback, onFailtCallback) {
                    _result.gridContent().busy('show');
                    unitlife.ajax.load(
                            {
                                url: url,
                                data: data,
                                success: function (data, s) {
                                    unitlife.ui.MessagePanel.show(data);

                                    if (typeof onSuccCallback == "function") {
                                        if (onSuccCallback.call) {
                                            onSuccCallback.call(this, data, s);
                                        }
                                    }
                                    _result.gridContent().busy('close');
                                },
                                complete: function (e, s) {
                                    _result.gridContent().busy('close');
                                },
                                error: function (e, s) {
                                    unitlife.ui.MessagePanel.show({ message: 'Ocurrió error en la operación.', isError: true });
                                    if (typeof onFailtCallback == "function") {
                                        if (onFailtCallback.call) {
                                            onFailtCallback.call(this, e, s);
                                        }
                                    }
                                    _result.gridContent().busy('close');
                                }
                            }
                        );
                },
                invoqCommandOpt: function (url,form, onSuccCallback, onFailtCallback) {
                    _result.gridContent().busy('show');
                    unitlife.ajax.load(
                        {
                            url: url,
                            //data: data,
                            form: form,
                            success: function (data, s) {
                                unitlife.ui.MessagePanel.show(data);

                                if (typeof onSuccCallback == "function") {
                                    if (onSuccCallback.call) {
                                        onSuccCallback.call(this, data, s);
                                    }
                                }
                                _result.gridContent().busy('close');
                            },
                            complete: function (e, s) {
                                _result.gridContent().busy('close');
                            },
                            error: function (e, s) {
                                unitlife.ui.MessagePanel.show({ message: 'Ocurrió error en la operación.', isError: true });
                                if (typeof onFailtCallback == "function") {
                                    if (onFailtCallback.call) {
                                        onFailtCallback.call(this, e, s);
                                    }
                                }
                                _result.gridContent().busy('close');
                            }
                        }
                    );
                },
                invoqCommandOptFile: function (url,data, onSuccCallback, onFailtCallback) {
                    _result.gridContent().busy('show');

                    $.ajax({
                        url: url,
                        type: "POST",
                        processData: false,
                        contentType: false,
                        data: data,
                        //form: form,
                        success: function (data, s) {
                            
                            //unitlife.ui.MessagePanel.show(data);
                            if (typeof onSuccCallback == "function") {
                                if (onSuccCallback.call) {
                                    onSuccCallback.call(this, data, s);
                                }
                            }
                            _result.gridContent().busy('close');
                         
                        },
                        error: function (e, s) {
                            unitlife.ui.MessagePanel.show({ message: 'Ocurrió error en la operación.', isError: true });
                            if (typeof onFailtCallback == "function") {
                                if (onFailtCallback.call) {
                                    onFailtCallback.call(this, e, s);
                                }
                            }
                            _result.gridContent().busy('close');
                        }

                    });


                    //unitlife.ajax.load(
                    //    {
                    //        url: url,
                    //        type: "POST",
                    //        processData: false,
                    //        contentType: false,
                    //        data: data,
                    //        //form: form,
                    //        success: function (data, s) {
                    //            unitlife.ui.MessagePanel.show(data);

                    //            if (typeof onSuccCallback == "function") {
                    //                if (onSuccCallback.call) {
                    //                    onSuccCallback.call(this, data, s);
                    //                }
                    //            }
                    //            _result.gridContent().busy('close');
                    //        },
                    //        complete: function (e, s) {
                    //            _result.gridContent().busy('close');
                    //        },
                    //        error: function (e, s) {
                    //            unitlife.ui.MessagePanel.show({ message: 'Ocurrió error en la operación.', isError: true });
                    //            if (typeof onFailtCallback == "function") {
                    //                if (onFailtCallback.call) {
                    //                    onFailtCallback.call(this, e, s);
                    //                }
                    //            }
                    //            _result.gridContent().busy('close');
                    //        }
                    //    }
                    //);
                },
                clearRows: function () {
                    this.gridElement().bootstrapTable('load', []);
                    _result.showDataInfo();
                    if (this.isFootter())
                        this.hideFooter();
                    this.resetLayout();
                },
                removeRow: function (id, value, onRemoveCallback) {
                    var _callback = true;
                    if (onRemoveCallback && typeof onRemoveCallback == "function") {
                        _callback = onRemoveCallback.call(this, { field: id, value: value });
                    }
                    if (_callback == false)
                        return;

                    this.gridElement().bootstrapTable('remove', { field: id, values: [value] });
                    _result.showDataInfo();
                    if (this.getData().length == 0 && this.isFootter())
                        this.hideFooter();
                    if (this.getData().length == 0)
                        this.clearNoFoundRows();
                    this.resetLayout();
                },
                addRow: function (data, onAddCallback) {
                    var _callback = true;
                    if (onAddCallback && typeof onAddCallback == "function") {
                        _callback = onAddCallback.call(this, { field: id, value: value });
                    }
                    if (_callback == false)
                        return;
                    if ($.isArray(data))
                        this.gridElement().bootstrapTable('append', data || []);
                    else
                        this.gridElement().bootstrapTable('append', [data || {}]);

                    _result.showDataInfo();
                    this.removeValidCss();
                    if (this.getData().length == 0 && this.isFootter())
                        this.hideFooter();
                    else if (this.getData().length > 0 && this.isFootter())
                        this.showFooter();
                    this.resetLayout();
                },
                updateRow: function (data, index) {
                    //console.log(data);
                    this.gridElement().bootstrapTable('updateRow', {
                        index: index,
                        row: data
                    });
                    _result.showDataInfo();
                },
                onCheckChanged: function (callback) {
                    this.gridElement().on('check.bs.table uncheck.bs.table ' +
                    'check-all.bs.table uncheck-all.bs.table', function (e, s) {
                        if (callback && typeof callback == "function") {
                            var sel = _result.getCheckedItems();
                            callback.call(this, { type: e.type || '', item: s || {}, selectedItems: sel, selectedItemsCount: sel.length });
                        }
                    });
                },
                getCheckedItems: function (field) {
                    if (field != undefined && field != '') {
                        var _checkedData = [];
                        var _data = this.getData();
                        $(_data).each(function (i, e) {
                            if (e[field] == true)
                                _checkedData.push(e);
                        });
                        return _checkedData;
                    }

                    return this.gridElement().bootstrapTable('getSelections');
                },
                getData: function () {
                    return this.gridElement().bootstrapTable('getData');
                },
                getDataRow: function (id) {
                    return this.gridElement().bootstrapTable('getRowByUniqueId', id);
                },
                getValueField: function (field) {
                    var _dta = this.getData();
                    var _arrVal = [];
                    for (var i = 0; i < _dta.length; i++) {
                        _arrVal.push(_dta[i][field]);
                    }
                    return _arrVal;
                },
                removeValidCss: function () {
                    this.gridElement().closest(".fixed-table-container").removeClass("input-validation-error");
                },
                saveRow: function (element, row) {
                    var exRo = $(element).val();
                    exRo = JSON.parse(exRo == '' ? "[]" : exRo);
                    exRo.push(row);

                    $(element).val(JSON.stringify(exRo));
                },
                getSavedRows: function (element) {
                    var exRo = $(element).val();
                    exRo = JSON.parse(exRo == '' ? "[]" : exRo);
                    return exRo;
                },
                removeAllSavedRow: function (element) {
                    $(element).val("");
                },
                removeSavedRow: function (element, key, value) {
                    var _r = false;
                    var _d = this.getSavedRows(element);
                    $(_d).each(function (i, e) {
                        if (e[key] == value) {
                            _r = e;
                            return false;
                        }
                    });
                    if (_r != false) {
                        _d.splice(_d.indexOf(_r), 1);
                    }
                    this.removeAllSavedRow(element);
                    $(element).val(JSON.stringify(_d));
                },
                showFooter: function () {
                    this.gridContent().find('.fixed-table-footer').show();
                },
                hideFooter: function () {
                    this.gridContent().find('.fixed-table-footer').hide();
                },
                isFootter: function () {
                    return this.gridElement().data('show-footer') == true;
                },
                validChecked: function (field) {
                    return unitlife.ui.validation.checkedValidGrid(element, field);
                },
                valid: function () {
                    return unitlife.ui.validation.validGrid(element);
                },
                resetValid: function () {
                    this.gridElement().closest(".fixed-table-container").removeClass("input-validation-error");
                },
                showOrHideColumn: function (colName, show /*true or false*/, colSubIndex) {
                    try {
                        // Ejemplo: var gridName = "tbListDet"; var colNameToHide = "Cantidad"; var colSubIndex = 0, show = true;

                        // Determinamos los indices de las columnas que cumplen con tener el [data-field] = colName
                        var columnsIndex = [];

                        this.gridElement().find("thead>tr>th").each(function (index, element) {
                            // $("#" + gridName + ">thead>tr>th").each(function (index, element) {
                            if ($(element).attr("data-field") == colName)
                                columnsIndex.push(index);
                        });

                        // Determinamos el INDICE FINAL de la columna requerida
                        var colFinalIndex = columnsIndex[colSubIndex];

                        // Ocultamos la celda de la CABECERA
                        this.gridElement().find("thead>tr>th").each(function (index, element) {
                            // $("#" + gridName + ">thead>tr>th").each(function (index, element) {
                            if (index == colFinalIndex) {
                                show ? $(element).show() : $(element).hide();
                                return;
                            }
                        });

                        // Ocultamos las celdas de las demás filas
                        this.gridElement().find("tbody>tr").each(function (index, element) {
                            // $("#" + gridName + ">tbody>tr").each(function(index, element) {
                            $(element).find("td").each(function (index2, element2) {
                                if (index2 == colFinalIndex) {
                                    show ? $(element2).show() : $(element2).hide();
                                    return;
                                }
                            });
                        });

                        // Ocultamos la celda del PIE DE PAGINA
                        this.gridElement().parent().parent().find("div[class='fixed-table-footer']>table>tbody>tr").each(function (index, element) {
                            // $("#" + gridName + ">tbody>tr").each(function(index, element) {
                            $(element).find("td").each(function (index2, element2) {
                                if (index2 == colFinalIndex) {
                                    show ? $(element2).show() : $(element2).hide();
                                    return;
                                }
                            });
                        });
                    } catch (e) {
                        unitlife.ui.MessagePanel.show({ isError: true, message: 'Error en la operación "showOrHideColumn"' });
                    }
                }
            };

            _result.gridContent().addClass('ui-grid');

            return _result;
        },
        string: function (value) {
            value = value == undefined ? "" : value;
            var _result = {
                trim: function () {
                    return value.replace(/^\s+|\s+$/g, '');
                },
                mtrim: function () {
                    return value.replace(/((?![\r\n])\s){2,}/g, ' ');
                }
            };
            return _result;
        },
        input: {
            onlyTexto: function (element, blurCallback) {
                $(element).on("keypress", function (e) {
                    return true;
                });

                $(element).on("blur", function (e) {
                    if (typeof blurCallback == "function") {
                        if (blurCallback.call) {
                            blurCallback.call(this, e);
                        }
                    }
                });

            },


            onlyDigit: function (element, blurCallback) {
                $(element).on("keypress", function (e) {

                    var keyCode = (e.which) ? e.which : e.keyCode;
                    if ((keyCode >= 48 && keyCode <= 57) || (keyCode == 8))
                        return true;
                    else if (keyCode == 46 || keyCode == 44) {
                        var curVal = document.activeElement.value;
                        if (curVal != null && (curVal.trim().indexOf('.') || curVal.trim().indexOf(',')) == -1)
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                });


                $(element).on("blur", function (e) {
                    var el = this;
                    var ex = /^[0-9]+\.?[0-9]*$/;
                    //if (ex.test(el.value) == false) {
                    //    el.value = el.value.substring(0, el.value.length - 1);
                    //}

                    var _val = parseFloat(el.value.split(',').join(''));
                    if (isNaN(_val))
                        this.value = 0;

                    this.value = unitlife.numeric.formatDecimal(this.value.split(',').join(''));

                    if (typeof blurCallback == "function") {
                        if (blurCallback.call) {
                            blurCallback.call(this, e);
                        }
                    }
                });
            },

            onlyDigit2: function (element, blurCallback) {
                $(element).on("keypress", function (e) {

                    var keyCode = (e.which) ? e.which : e.keyCode;
                    if ((keyCode >= 48 && keyCode <= 57) || (keyCode == 8))
                        return true;
                    else if (keyCode == 46 || keyCode == 44) {
                        var curVal = document.activeElement.value;
                        if (curVal != null && (curVal.trim().indexOf('.') || curVal.trim().indexOf(',')) == -1)
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                });


                $(element).on("blur", function (e) {
                    var el = this;
                    var ex = /^[0-9]+\.?[0-9]*$/;
                    //if (ex.test(el.value) == false) {
                    //    el.value = el.value.substring(0, el.value.length - 1);
                    //}

                    var _val = parseFloat(el.value.split(',').join(''));
                    if (isNaN(_val))
                        this.value = 0;

                    this.value = unitlife.numeric.formatDecimal(this.value.split(',').join(''), 3);

                    if (typeof blurCallback == "function") {
                        if (blurCallback.call) {
                            blurCallback.call(this, e);
                        }
                    }
                });
            },
            onlyDigit6: function (element, blurCallback) {
                $(element).on("keypress", function (e) {

                    var keyCode = (e.which) ? e.which : e.keyCode;
                    if ((keyCode >= 48 && keyCode <= 57) || (keyCode == 8))
                        return true;
                    else if (keyCode == 46 || keyCode == 44) {
                        var curVal = document.activeElement.value;
                        if (curVal != null && (curVal.trim().indexOf('.') || curVal.trim().indexOf(',')) == -1)
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                });


                $(element).on("blur", function (e) {
                    var el = this;
                    var ex = /^[0-9]+\.?[0-9]*$/;
                    //if (ex.test(el.value) == false) {
                    //    el.value = el.value.substring(0, el.value.length - 1);
                    //}

                    var _val = parseFloat(el.value.split(',').join(''));
                    if (isNaN(_val))
                        this.value = 0;

                    this.value = unitlife.numeric.formatDecimal(this.value.split(',').join(''), 6);

                    if (typeof blurCallback == "function") {
                        if (blurCallback.call) {
                            blurCallback.call(this, e);
                        }
                    }
                });
            },
            onlyNumeric: function (element, blurCallback) {
                $(element).on("keypress", function (e) {
                    var keyCode = (e.which) ? e.which : e.keyCode;
                    if ((keyCode >= 48 && keyCode <= 57) || (keyCode == 8))
                        return true;
                    else
                        return false;
                });

                $(element).on("blur", function (e) {
                    var el = this;
                    var ex = /^[0-9\b]+$/;
                    if (ex.test(el.value) == false) {
                        el.value = el.value.substring(0, el.value.length - 1);
                    }

                    var _val = parseInt(el.value);
                    if (isNaN(_val)) {
                        var dss = $(this).data('invalid-default-value');
                        if (dss == undefined)
                            this.value = 0;
                        else
                            this.value = dss;
                    }


                    if (typeof blurCallback == "function") {
                        if (blurCallback.call) {
                            blurCallback.call(this, e);
                        }
                    }
                });
            },
            onlyDigit7: function (element, blurCallback) {
                $(element).on("keypress", function (e) {

                    ////var curValDec = document.activeElement.value;
                    //var character = String.fromCharCode(e.keyCode)
                    //var newValue = this.value + character;

                    //var pointIndex = value.indexOf('.')
                    //if(curVal.trim().indexOf('.')) = -

                    var keyCode = (e.which) ? e.which : e.keyCode;
                    if ((keyCode >= 48 && keyCode <= 57) || (keyCode == 8))
                        return true;
                    else if (keyCode == 46 || keyCode == 44 || keyCode == 109 || keyCode ==  45 ) {
                        var curVal = document.activeElement.value;
                        if (curVal != null && (curVal.trim().indexOf('.') || curVal.trim().indexOf(',') || curVal.trim().indexOf('-')) == -1)
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                });


                $(element).on("blur", function (e) {
                    var el = this;
                    var ex = /^[0-9]+\.?[0-9]*$/;
                    //if (ex.test(el.value) == false) {
                    //    el.value = el.value.substring(0, el.value.length - 1);
                    //}
                    
                    var _val = parseFloat(el.value.split(',').join('').replace('-',''));
                    if (isNaN(_val))
                        this.value = 0;

                    this.value = unitlife.numeric.formatDecimalTrunca(this.value.split(',').join(''), 7);

                    if (typeof blurCallback == "function") {
                        if (blurCallback.call) {
                            blurCallback.call(this, e);
                        }
                    }
                });
            },



        },
        sidePanel: function (element, onExpandCallback, onHideCallback) {
            var sid = $(element);
            var _onExpandCallback = onExpandCallback;
            var _onHideCallback = onHideCallback;

            var _result = {
                create: function () {
                    sid.find('.sidebar-sidePanel .toggled-ico').unbind("click");
                    sid.find('.sidebar-sidePanel .toggled-ico').bind("click", function (e, s) {
                        var isExpd = $(element).hasClass("toggled");
                        var _toIico = $(this).find("span");
                        if (isExpd) {
                            _result.collapse();
                            _toIico.removeClass("glyphicon-menu-right");
                            _toIico.addClass("glyphicon-menu-left");
                        }
                        else {
                            _result.expand();
                            _toIico.removeClass("glyphicon-menu-left");
                            _toIico.addClass("glyphicon-menu-right");
                        }
                    });
                },
                expand: function () {
                    sid.addClass("toggled");
                    if (typeof _onExpandCallback == "function") {
                        if (_onExpandCallback.call) {
                            _onExpandCallback.call(this);
                        }
                    }
                },
                collapse: function () {
                    sid.removeClass("toggled");
                    if (typeof _onHideCallback == "function") {
                        if (_onHideCallback.call) {
                            _onHideCallback.call(this);
                        }
                    }
                }
            };

            return _result;
        },
        format: {
            actionText: function (element, value) {
                var da = $(element).data("textformat");
                if (da == undefined)
                    return;
                da = da.replace("{0}", value);
                $(element).html(da);
            }
        },
        form: function (form) {
            var _fmr = $(form);
            var fields = $("input, textarea, select", _fmr);

            var _result = {
                clear: function (controls) {
                    controls = controls || _fmr.find('input');
                    $(controls).each(function (i, e) {
                        var _inpt = _fmr.find(e);
                        _inpt.val('');
                    });
                },
                fill: function (item, tagPro) {
                    var _d = tagPro;
                    $(fields).each(function (i, e) {

                        var _name = e.name;
                        if (tagPro != undefined && _name.indexOf('.') != -1) {
                            var sa = _name.split('.')[0];
                            if (sa == tagPro) {
                                _name = _name.split('.')[1];
                                $(e).val(item[_name] == undefined ? $(e).val() : item[_name]);
                            }
                        }
                        else if (tagPro == undefined) {
                            $(e).val(item[_name] || $(e).val());
                        }

                    });
                },
                reset: function (elements, excludeElements) {
                    elements = elements == undefined || elements == 'all' ? fields : elements;
                    excludeElements = excludeElements || [];
                    $(elements).each(function (i, e) {
                        var _id = e.id ? e.id : e;
                        if (excludeElements.indexOf(_id) == -1)
                            $(e).val('');
                    });
                },
                fillJson: function (url, tagProp, onSuccCallback) {
                    tagProp = tagProp || "";

                    var _ajax = unitlife.ajax;
                    var _this = unitlife.ui;
                    var _onSuccCallback = function (e, s) {
                        if (e.isError) {
                            unitlife.ui.MessagePanel.show(e);
                        }
                        _result.fill(e.data || {}, tagProp);

                        if (typeof onSuccCallback == "function") {
                            if (onSuccCallback.call) {
                                onSuccCallback.call(this, e, s);
                            }
                        }
                    }, onError = function (e) {
                        unitlife.ui.MessagePanel.show({ message: 'Ocurrió error en la operación', isError: true });
                    };

                    _ajax.getJson(url, _onSuccCallback, onError);
                },
                checkInputs: function () {
                    var _controls = $("input[type=text], textarea, select", _fmr);
                    _controls.each(function (i, e) {
                        e.value = unitlife.ui.string(e.value).trim();
                        e.value = unitlife.ui.string(e.value).mtrim();
                    });
                }
            }
            return _result;
        }

    },
    numeric: {
        //formatDecimal: function (value, decimals, decpoint, sep) {

        //    var _opt = {
        //        decPoint: decpoint || '.',
        //        separator: sep || ',',
        //        number: value || 0,
        //        decimal: decimals || 2
        //    };

        //    _opt.number = (_opt.number + '').replace(/[^0-9+-Ee.]/g, '');
        //    var n = !isFinite(+_opt.number) ? 0 : +_opt.number,
        //        prec = !isFinite(+_opt.decimal) ? 0 : Math.abs(_opt.decimal),
        //        sep = (typeof _opt.separator === 'undefined') ? ',' : _opt.separator,
        //        dec = (typeof _opt.decPoint === 'undefined') ? '.' : _opt.decPoint,
        //        s = '',
        //        toFixedFix = function (n, prec) {
        //            var k = Math.pow(10, prec);
        //            return '' + Math.round(n * k) / k;
        //        };

        //    s = (prec ? toFixedFix(n, prec) : '' + Math.round(n)).split('.');
        //    if (s[0].length > 3) {
        //        s[0] = s[0].replace(/B(?=(?:d{3})+(?!d))/g, sep);
        //    }
        //    if ((s[1] || '').length < prec) {
        //        s[1] = s[1] || '';
        //        s[1] += new Array(prec - s[1].length + 1).join('0');
        //    }
        //    return s.join(dec);
        //}
        formatDecimal: function (numero, decimales, separadorDecimal, separadorMiles) {

            var partes, array;

            if (!isFinite(numero) || isNaN(numero = parseFloat(numero))) {
                return "";
            }
            if (typeof decimales === "undefined") {
                decimales = 2;
            }
            if (typeof separadorDecimal === "undefined") {
                separadorDecimal = ".";
            }
            if (typeof separadorMiles === "undefined") {
                separadorMiles = ",";
            }
            // Redondeamos
            if (!isNaN(parseInt(decimales))) {
                if (decimales >= 0) {
                    numero = numero.toFixed(decimales);
                } else {
                    numero = (
                        Math.round(numero / Math.pow(10, Math.abs(decimales))) * Math.pow(10, Math.abs(decimales))
                    ).toFixed();
                }
            } else {
                numero = numero.toString();
            }
            // Damos formato
            partes = numero.split(".", 2);
            array = partes[0].split("");
            for (var i = array.length - 3; i > 0 && array[i - 1] !== "-"; i -= 3) {
                array.splice(i, 0, separadorMiles);
            }
            numero = array.join("");

            if (partes.length > 1) {
                numero += separadorDecimal + partes[1];
            }
            return numero;
        },
        formatDecimalTrunca: function (numero, decimales, separadorDecimal, separadorMiles) {

            var partes, array;
            var parteDecimalStr;
            var parteDecimal;
            var decimalTrun;

            if (!isFinite(numero) || isNaN(numero = parseFloat(numero))) {
                return "";
            }
            if (typeof decimales === "undefined") {
                decimales = 2;
            }
            if (typeof separadorDecimal === "undefined") {
                separadorDecimal = ".";
            }
            if (typeof separadorMiles === "undefined") {
                separadorMiles = ",";
            }
            // Redondeamos
            if (!isNaN(parseInt(decimales))) {

                if (decimales >= 0) {
                    numero = numero.toString();
                    partes = numero.split(".", 2);

                    if (numero.trim().indexOf('.') != -1) {
                        parteDecimal = partes[1];
                        parteDecimalStr = new String(parteDecimal);
                        decimalTrun =  '.'+ parteDecimalStr.substring(0, decimales);
                    } else {

                        decimalTrun = "";

                    }
                    
                   
                    //decimal = decimal.substring(0, decimales);
                    //var exp = Math.pow(10, decimales || 2);
                    numero = partes[0] + decimalTrun;//(parseFloat(numero * exp, 10) / exp).toString();

                  //  numero = numero.toFixed(decimales);
                } else {
                    numero = (
                        Math.round(numero / Math.pow(10, Math.abs(decimales))) * Math.pow(10, Math.abs(decimales))
                    ).toFixed();
                }



            } else {
                numero = numero.toString();
            }
            // Damos formato
            //partes = numero.split(".", 2);
            //array = partes[0].split("");
            //for (var i = array.length - 3; i > 0 && array[i - 1] !== "-"; i -= 3) {
            //    array.splice(i, 0, separadorMiles);
            //}
            //numero = array.join("");

            //if (partes.length > 1) {
            //    numero += separadorDecimal + partes[1];
            //}
            return numero;
        }
    },
    downloader: function (form) {
        form = form == undefined ? false : form;
        var _isForm = form == false ? false : $(form).is("form");

        var _backUrl = _isForm == true ? $(form).attr("action") : null;
        var _form = _isForm == true ? $(form) : null;

        var result = {
            download: function (url) {
                var _ifmr = $(document.body).find('#_dialFrameDownloader');
                if (_ifmr.length == 0)
                    $('<iframe id="_dialFrameDownloader" name="_dialFrameDownloader" class="ui-part-page"></iframe>').appendTo($(document.body));
                _ifmr = $(document.body).find('#_dialFrameDownloader');
                if (_isForm == true) {
                    _form.attr("target", "_dialFrameDownloader");
                    _form.attr("action", url);
                    _form.get(0).submit();
                }
                else
                    _ifmr.get(0).src = url;

                _ifmr.on("load", function (e) {
                    try {
                        var da = JSON.parse($(this).contents().find('body').text() || {});
                        unitlife.ui.MessagePanel.show(da)
                    } catch (e) {
                        unitlife.ui.MessagePanel.show({ isError: true, message: 'Error en la operación' });
                    }
                });
            }
        };
        return result;
    }
}

unitlife.ui.grid.commands = {
    remove: function () { return '<a id="remove" href="javascript:void(0)" title="Delete Record"><span class="glyphicon glyphicon-trash" aria-hidden="true"></span></a>'; },
    add: function () { return '<a id="add" href="javascript:void(0)" title="Add"><span class="glyphicon glyphicon-plus" aria-hidden="true"></span></a>'; },
    view: function () { return '<a id="view" href="javascript:void(0)" title="Visualize"><span class="glyphicon glyphicon-time" aria-hidden="true"></span></a>'; },
    edit: function () { return '<a id="edit" href="javascript:void(0)" title="Edit"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span></a>'; },
    editPassword: function () { return '<a id="editPassword" href="javascript:void(0)"  title="Generate password"><span class="glyphicon glyphicon-user" aria-hidden="true"></span></a>'; },
    asignarPerfil: function () { return '<a id="asignarPerfil" href="javascript:void(0)"  title="Assign profile"><span class="glyphicon glyphicon-user" aria-hidden="true"></span></a>'; },
    CrearUsuario: function () { return '<a id="CrearUsuario" href="javascript:void(0)"  title="Generate user account"><span class="glyphicon glyphicon-user" aria-hidden="true"></span></a>'; },
    "delete": function () { return '<a id="delete" href="javascript:void(0)" title="Remove record"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></a>'; },
    detalle: function () { return '<a id="detalle" href="javascript:void(0)"  title="Registration detail"><span class="glyphicon glyphicon-list-alt" aria-hidden="true"></span></a>'; },
    email: function () { return '<a id="email" href="javascript:void(0)" title="Send mail"><span class="glyphicon glyphicon-envelope" aria-hidden="true"></span></a>'; },
    pdf: function () { return '<a id="pdf" href="javascript:void(0)" title="pdf"><span class="glyphicon glyphicon-cloud-download" aria-hidden="true"></span></a>'; },
    onlyview: function () { return '<a id="onlyview" href="javascript:void(0)" title="Check registration"><span class="glyphicon glyphicon-info-sign" aria-hidden="true"></span></a>'; },
    xml: function () { return '<a id="xml" href="javascript:void(0)" title="xml"><span class="glyphicon glyphicon-link" aria-hidden="true"></span></a>'; },
    list: function () { return '<a id="list" href="javascript:void(0)" title="Detail list"><span class="glyphicon glyphicon-th-list" aria-hidden="true"></span></a>'; },
    clonar: function () { return '<a id="Clonar" href="javascript:void(0)" title="Clone"><span class="glyphicon glyphicon-repeat" aria-hidden="true"></span></a>'; },
    Adjuntos: function () { return '<a id="Adjuntos" href="javascript:void(0)" title="See attachments"><span class="glyphicon glyphicon-paperclip" aria-hidden="true"></span></a>'; },
    descargar: function () { return '<a id="descargar" href="javascript:void(0)" title="Download"><span class="glyphicon glyphicon-cloud-download" aria-hidden="true"></span></a>'; },
    denegate: function () { return '<a id="denegate" href="javascript:void(0)" title="Denegate"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></a>'; },
    accept: function () { return '<a id="accept" href="javascript:void(0)" title="Accept"><span class="glyphicon glyphicon-ok" aria-hidden="true"></span></a>'; },
    pagar: function () { return '<a id="pagar" href="javascript:void(0)" title="Pay"><span class="glyphicon glyphicon-usd" aria-hidden="true"></span></a>'; },
    donations: function () { return '<a id="donations" href="javascript:void(0)" title="Donations"><span class="glyphicon glyphicon-heart" aria-hidden="true"></span></a>'; },
    eyes: function () { return '<a id="eyes" href="javascript:void(0)" title="View TimeLine"><span class="glyphicon glyphicon-eye-open" aria-hidden="true"></span></a>'; },
    finance: function () { return '<a id="finance" href="javascript:void(0)" title="Financial data"><span class="glyphicon glyphicon-usd" aria-hidden="true"></span></a>'; },
    control: function () { return '<a id="control" href="javascript:void(0)" title="Controls"><span class="glyphicon glyphicon-cog" aria-hidden="true"></span></a>'; },
    globe: function () { return '<a id="globe" href="javascript:void(0)" title="Translation"><span class="glyphicon glyphicon-globe" aria-hidden="true"></span></a>'; },
    timeLine: function () { return '<a id="timeLine" href="javascript:void(0)" title="Time Line"><span class="glyphicon glyphicon-plus-sign" aria-hidden="true"></span></a>'; }
   
}

unitlife.ui.grid.global = {
    rowState: {
        activo: '1',
        inactivo: '0',
        registrado: 'REG',
        pendiente: 'PDE',
        observado: 'OBS',
        aprobado: 'APR',
        anulado: 'ANU',
        rechazado: 'REC',
        errorEmisor: 'ERE',
        errorSunat: 'ERS',
        sinEstado: ''
    }
};

unitlife.ui.grid.rowStateGenericStyle = function (row, index, field) {

    field = field == undefined ? (row.Status == undefined ? "" : row.Status) : field;

    if (field == unitlife.ui.grid.global.rowState.inactivo
        | field == unitlife.ui.grid.global.rowState.rechazado
        | field == unitlife.ui.grid.global.rowState.errorEmisor
        //| field == unitlife.ui.grid.global.rowState.errorSunat
        ) {
        return {
            classes: "row-red"
        };
    }
    else if (field == unitlife.ui.grid.global.rowState.observado) {
        return {
            classes: "row-ambar"
        };
    }
    else if (field == unitlife.ui.grid.global.rowState.aprobado) {
        return {
            classes: "row-green"
        };
    }
    else if (field == unitlife.ui.grid.global.rowState.anulado) {
        return {
            classes: "row-green-red"
        };
    }
    else if (field == unitlife.ui.grid.global.rowState.pendiente) {
        return {
            classes: ""
        };
    }
    else if (field == unitlife.ui.grid.global.rowState.sinEstado) {
        return {
            classes: ""
        };
    }
    else if (row.CorreoValido == 0 && row.CorreoError == 0) {
        return {
            classes: "row-ambar"
        };
    }
    else if (row.CorreoValido > 0) {
        return {
            classes: "info"
        };
    }
    else if (row.CorreoValido == 0 && row.CorreoError > 0) {
        return {
            classes: "row-red"
        };
    }
    return {};
};

unitlife.ui.grid.rowDigitGenericStyle = function (value, index, field) {
    var _cell = $(value);
    if (_cell.hasClass('ui-cell-input')) {
        return {
            classes: 'ui-cell-input-col cell-digit'
        };
    }

    return {};
}

unitlife.ui.grid.cellInputCuatroDeci = function (value, colName, readonly) {

    readonly = readonly || false;
    var _input = '<input id="editCell" type="text" class="form-control input-sm ui-cell-input" ' +
        'value="' + unitlife.numeric.formatDecimal(value, 4, '.', ',') + '" ' +
        ' data-colname="' + colName + '"' +
        (readonly == true ? ' readonly="true"' : '') +
        '/>';
    return _input;
}

unitlife.ui.grid.cellInput = function (value, colName, readonly) {

    readonly = readonly || false;
    var _input = '<input id="editCell" type="text" class="form-control input-sm ui-cell-input" ' +
        'value="' + unitlife.numeric.formatDecimal(value, 2 /* 3 -- Anteriormente este valor ocasionaba que las cajas de texto ingresaran 3 decimales */) + '" ' +
        ' data-colname="' + colName + '"' +
        (readonly == true ? ' readonly="true"' : '') +
        '/>';
    return _input;
}

unitlife.ui.grid.cellInputNumber = function (value, colName, readonly) {
    readonly = readonly || false;
    var _input = '<input id="editCellNum" type="text" class="form-control input-sm ui-cell-input" ' +
        'value="' + value + '" ' +
        ' data-colname="' + colName + '"' +
        (readonly == true ? ' readonly="true"' : '') +
        '/>';
    return _input;
}

unitlife.ui.grid.cellInputOnlyDigits = function (value, colName, readonly) {

    readonly = readonly || false;
    var _input = '<input id="editCell" type="text" class="form-control input-sm ui-cell-input" ' +
        'value="' + unitlife.numeric.formatDecimal(value, 0) + '" ' +
        ' data-colname="' + colName + '"' +
        (readonly == true ? ' readonly="true"' : '') +
        '/>';
    return _input;
}

unitlife.ui.grid.cellCheckbox = function (value, colName) {
    var _input = '<input id="check" type="checkbox" class="ui-cell-input" ' +
        (value == true ? 'checked="checked"' : '') +
        'data-colname="' + (typeof this == "object" && this.field != undefined ? this.field : colName) + '"' +
        '/>';
    return _input;
}

unitlife.ui.grid.cellDisableCheckbox = function (value, colName, disable) {
    var _input = '<input id="check" type="checkbox" class="ui-cell-input" ' +
        (value == true ? 'checked="checked"' : '') +
        (disable == true ? 'disabled="disabled"' : '') +
        'data-colname="' + (typeof this == "object" && this.field != undefined ? this.field : colName) + '"' +
        '/>';
    return _input;
}

unitlife.ui.grid.cellInputText = function (value, colName) {
    var _input = '<input id="editCellText" type="text" class="form-control input-sm ui-cell-input" ' +
        'value="' + value + '" ' +
        'data-colname="' + colName + '"' +
        '/>';
    return _input;
}

unitlife.ui.grid.cellInputTextR = function (value, colName, textRequired) {
    var _input = '<input id="editCellText" type="text" class="form-control input-sm ui-cell-input" ' +
        'value="' + value + '" ' +
        'data-colname="' + colName + '" data-val-required="' + textRequired + '" data-val="true" ' +
        '/>';
    return _input;
}

unitlife.ui.grid.cellInputTextE = function (value, colName, readonly) {
    readonly = readonly || false;
    var _input = '<input id="editCellText" type="text" class="form-control input-sm ui-cell-input" ' +
        'value="' + value + '" ' +
       ' data-colname="' + colName + '"' +
        (readonly == true ? ' readonly="true"' : '') +
        '/>';
    return _input;
}

unitlife.ui.grid.cellInputTextLeng = function (value, colName, readonly) {
    readonly = readonly || false;
    var _input = '<input id="editCellText" type="text" maxlength="250" class="form-control input-sm ui-cell-input" ' +
        'value="' + value + '" ' +
        ' data-colname="' + colName + '"' +
        (readonly == true ? ' readonly="true"' : '') +
        '/>';
    return _input;
}

unitlife.ui.grid.cellInputTextM = function (value, colName, readonly) {
    readonly = readonly || false;
    var _input = '<textarea  id="editCellText2" rows="5" maxlength="250" class="form-control input-sm ui-cell-input" ' +
        'value="' + value + '" ' +
       ' data-colname="' + colName + '"' +
        (readonly == true ? ' readonly="true"' : '') +
        '/>';
    return _input;
}




unitlife.ui.grid.aggregate = {
    sum: function (data, field) {

        field = field == undefined ? this.field : field;

        var _res = 0; //IE-8
        $(data).each(function (i, e) {
            _res = _res + (+e[field]);
        });

        //var _res = data.reduce(function (sum, row) {
        //    return sum + (+row[field]);
        //}, 0);

        if (isNaN(_res) || _res == 0)
            _res = 0;
        return unitlife.numeric.formatDecimal(_res, 2, '.', ',');
    },
    sum4: function (data) {
        field = this.field;

        var _res = 0; //IE-8
        $(data).each(function (i, e) {
            _res = _res + (+e[field]);
        });

        //var _res = data.reduce(function (sum, row) {
        //    return sum + (+row[field]);
        //}, 0);

        if (isNaN(_res) || _res == 0)
            _res = 0;
        return unitlife.numeric.formatDecimal(_res, 4, '.', ',');
    }
}

unitlife.ui.grid.formatter = {
    numeroEntero: function (e, r, i) {
        return unitlife.numeric.formatDecimal(r[this.field], 0, '.', ',');
    },
    decimal: function (e, r, i) {
        return unitlife.numeric.formatDecimal(r[this.field], 2, '.', ',');
    },
    decimal4: function (e, r, i) {
        return unitlife.numeric.formatDecimal(r[this.field], 4, '.', ',');
    }
}

unitlife.ui.validation.showMessage = function (msg) {
    msg = msg || "Fields framed in red are required.";

    unitlife.ui.MessagePanel.show({ isError: true, message: msg });
}

unitlife.ui.validation.validGrid = function (grid) {
    var gs = unitlife.ui.grid(grid);
    var _result = false;
    if (gs.getData().length == 0) {
        var _cont = gs.gridElement().closest(".fixed-table-container");
        _cont.addClass("input-validation-error");

        _result = false;
    }
    else {
        gs.gridElement().closest(".fixed-table-container").removeClass("input-validation-error");
        _result = true;
    }

    if (_result == false)
        unitlife.ui.validation.showMessage();
    else
        unitlife.ui.MessagePanel.hide();

    return _result;
}

unitlife.ui.validation.checkedValidGrid = function (grid, field) {
    var gs = unitlife.ui.grid(grid);
    var _result = false;
    if (gs.getCheckedItems(field).length == 0) {
        var _cont = gs.gridElement().closest(".fixed-table-container");
        _cont.addClass("input-validation-error");

        _result = false;
    }
    else {
        gs.gridElement().closest(".fixed-table-container").removeClass("input-validation-error");
        _result = true;
    }

    if (_result == false)
        unitlife.ui.validation.showMessage();
    else
        unitlife.ui.MessagePanel.hide();

    return _result;
}

unitlife.browser = function () {

    var nVer = navigator.appVersion;
    var nAgt = navigator.userAgent;
    var browserName = navigator.appName;
    var fullVersion = '' + parseFloat(navigator.appVersion);
    var majorVersion = parseInt(navigator.appVersion, 10);
    var nameOffset, verOffset, ix;

    // In Opera, the true version is after "Opera" or after "Version"
    if ((verOffset = nAgt.indexOf("Opera")) != -1) {
        browserName = "Opera";
        fullVersion = nAgt.substring(verOffset + 6);
        if ((verOffset = nAgt.indexOf("Version")) != -1)
            fullVersion = nAgt.substring(verOffset + 8);
    }
        // In MSIE, the true version is after "MSIE" in userAgent
    else if ((verOffset = nAgt.indexOf("MSIE")) != -1) {
        browserName = "Microsoft Internet Explorer";
        fullVersion = nAgt.substring(verOffset + 5);
    }
        // In Chrome, the true version is after "Chrome" 
    else if ((verOffset = nAgt.indexOf("Chrome")) != -1) {
        browserName = "Chrome";
        fullVersion = nAgt.substring(verOffset + 7);
    }
        // In Safari, the true version is after "Safari" or after "Version" 
    else if ((verOffset = nAgt.indexOf("Safari")) != -1) {
        browserName = "Safari";
        fullVersion = nAgt.substring(verOffset + 7);
        if ((verOffset = nAgt.indexOf("Version")) != -1)
            fullVersion = nAgt.substring(verOffset + 8);
    }
        // In Firefox, the true version is after "Firefox" 
    else if ((verOffset = nAgt.indexOf("Firefox")) != -1) {
        browserName = "Firefox";
        fullVersion = nAgt.substring(verOffset + 8);
    }
        // In most other browsers, "name/version" is at the end of userAgent 
    else if ((nameOffset = nAgt.lastIndexOf(' ') + 1) <
              (verOffset = nAgt.lastIndexOf('/'))) {
        browserName = nAgt.substring(nameOffset, verOffset);
        fullVersion = nAgt.substring(verOffset + 1);
        if (browserName.toLowerCase() == browserName.toUpperCase()) {
            browserName = navigator.appName;
        }
    }
    // trim the fullVersion string at semicolon/space if present
    if ((ix = fullVersion.indexOf(";")) != -1)
        fullVersion = fullVersion.substring(0, ix);
    if ((ix = fullVersion.indexOf(" ")) != -1)
        fullVersion = fullVersion.substring(0, ix);

    majorVersion = parseInt('' + fullVersion, 10);
    if (isNaN(majorVersion)) {
        fullVersion = '' + parseFloat(navigator.appVersion);
        majorVersion = parseInt(navigator.appVersion, 10);
    }

    return {
        browserName: browserName,
        fullVersion: fullVersion,
        majorVersion: majorVersion,
        appName: navigator.appName,
        userAgent: navigator.userAgent
    };
}

function Confirmar(mensaje, handlerAceptar, handlerCancelar) {
    $('#confirmMensaje').text(mensaje);
    if (!handlerAceptar) {
        $('#btnConfirmarAceptar').unbind('click').click(function () { ConfirmarOcultar(); });
    } else {
        $('#btnConfirmarAceptar').unbind('click').click(function () { handlerAceptar(); ConfirmarOcultar(); });
    }
    if (!handlerCancelar) {
        $('#btnConfirmarCancelar').unbind('click').click(function () { ConfirmarOcultar(); });
    } else {
        $('#btnConfirmarCancelar').unbind('click').click(function () { handlerCancelar(); ConfirmarOcultar(); });
    }
    $('#dialogConfirm').modal('show');
}

function ConfirmarOcultar() {
    $('#dialogConfirm').modal('hide');
    $('#btnConfirmarAceptar, #btnConfirmarCancelar').unbind('click');
    $('#confirmMensaje').empty();
}