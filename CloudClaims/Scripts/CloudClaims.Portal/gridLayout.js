(function (skillet, $, undefined) {
    $(function () {
        //#region GLOBAL CONSTANTS BEGIN
        var HTML_Layout = new Array();
        HTML_Layout['a'] = '<ul>' +
            '<li id="widget1" class="widgetx" data-type="CONTENT" data-row="1" data-col="1" data-sizex="3" data-sizey="2" data-elementtype="notset" data-elementtypeid=""></li>' +
            '<li id="widget2" class="widgetx" data-type="CONTENT" data-row="1" data-col="4" data-sizex="3" data-sizey="2" data-elementtype="notset" data-elementtypeid=""></li>' +
            '</ul>';

        HTML_Layout['b'] = '<ul>' +
            '<li id="widget1" class="widgetx" data-type="CONTENT" data-row="1" data-col="1" data-sizex="2" data-sizey="2" data-elementtype="notset" data-elementtypeid=""></li>' +
            '<li id="widget2" class="widgetx" data-type="CONTENT" data-row="1" data-col="3" data-sizex="2" data-sizey="2" data-elementtype="notset" data-elementtypeid=""></li>' +
            '<li id="widget3" class="widgetx" data-type="CONTENT" data-row="1" data-col="5" data-sizex="2" data-sizey="2" data-elementtype="notset" data-elementtypeid=""></li>' +
            '</ul>';

        HTML_Layout['c'] = '<ul>' +
            '<li id="widget1" class="widgetx" data-type="CONTENT" data-row="1" data-col="1" data-sizex="2" data-sizey="2" data-elementtype="notset" data-elementtypeid=""></li>' +
            '<li id="widget2" class="widgetx" data-type="CONTENT" data-row="1" data-col="3" data-sizex="4" data-sizey="2" data-elementtype="notset" data-elementtypeid=""></li>' +
            '</ul>';

        HTML_Layout['d'] = '<ul>' +
            '<li id="widget1" class="widgetx" data-type="CONTENT" data-row="1" data-col="1" data-sizex="4" data-sizey="2" data-elementtype="notset" data-elementtypeid=""></li>' +
            '<li id="widget2" class="widgetx" data-type="CONTENT" data-row="1" data-col="5" data-sizex="2" data-sizey="2" data-elementtype="notset" data-elementtypeid=""></li>' +
            '</ul>';

        HTML_Layout['e'] = '<ul>' +
            '<li id="widget1" class="widgetx" data-type="CONTENT" data-row="1" data-col="1" data-sizex="6" data-sizey="2" data-elementtype="notset" data-elementtypeid=""></li>' +
            '</ul>';

        var HTML_WidgetContent = '<div class="widget-content"></div>';

        var HTML_WidgetOptions = '' +
            '<div class="widget-options-wrap">' +
            '<img class="widget-options" src="' + GLOBAL_CONTENTS_URL + 'fatcow-icons/cog.png" alt="" />' +
            '<img class="widget-options-delete" src="' + GLOBAL_CONTENTS_URL + 'fatcow-icons/application_delete.png" alt="" />' +
            '<img class="widget-options-accept" src="' + GLOBAL_CONTENTS_URL + 'fatcow-icons/accept.png" alt="" />' +
            '</div>';

        var HTML_InjectElement = new Array();
        HTML_InjectElement['textbox'] = '<div class="formRow">' +
                                            '<div class="span12">' +
                                                '<input type="hidden" id="" />' +
                                                '<label></label>' +
                                                '<input type="text" />' +
                                            '</div>' +
                                            '<div class="clear"></div>' +
                                        '</div>';

        HTML_InjectElement['dropdown'] = '<div class="formRow">' +
                                            '<div class="oneFour">' +
                                                '<input type="hidden" id="" />' +
                                                '<label></label><br />' +
                                                '<select disabled="disabled"><option>Choose an option..</option></select>' +
                                            '</div>' +
                                            '<div class="clear"></div>' +
                                        '</div>';

        HTML_InjectElement['datefield'] = '<div class="formRow">' +
                                                '<div class="oneTwo">' +
                                                    '<input type="hidden" id="" />' +
                                                    '<img src="' + GLOBAL_CONTENTS_URL + 'fatcow-icons/calendar.png" alt="" class="icon" width="16" />' +
                                                    '<label></label>' +
                                                    '<input type="text" class="datepicker" />' +
                                                '</div>' +
                                                '<div class="clear"></div>' +
                                            '</div>';

        HTML_InjectElement['checkbox'] = '<div class="formRow">' +
                                            '<div class="span12">' +
                                                '<input type="hidden" id="" />' +
                                                '<label></label>' +
                                                '<input type="checkbox" />' +
                                            '</div>' +
                                            '<div class="clear"></div>' +
                                        '</div>';

        HTML_InjectElement['personnel'] = '<div class="formRow">' +
                                            '<div class="span12">' +
                                                '<input type="hidden" id="" />' +
                                                '<img src="' + GLOBAL_CONTENTS_URL + 'fatcow-icons/user_gray.png" alt="" class="icon" width="16" />' +
                                                '<label></label>' +
                                                '<input type="text" />' +
                                            '</div>' +
                                            '<div class="clear"></div>' +
                                        '</div>';


        var gridster;
        var ACTIVE_WIDGET_ID = -1;
        var ACTIVE_WIDGET_LOCK = false;
        var WIDGET_WIDTH; // = Math.ceil(($('.gridster').width() / 6) - 2); //Math.ceil(($('.gridster').width() / 6) - 2);
        var WIDGET_HEIGHT = 40;
        var grid_margin = 0;
        var block_params = {
            max_width: 6,
            max_height: 6
        };

        var widgetOptionsToggleState = new Array();
        var footerButtonActive = new Array();
        footerButtonActive[0] = false;  //Pick a layout
        footerButtonActive[1] = false; //Add content block
        footerButtonActive[2] = false; //Image library
        //#endregion

        //Used for gridster and jQuery UI resizable hanadshaking
        function resizeBlock(elmObj) {
            var elmObj = $(elmObj);
            var w = elmObj.width() - (WIDGET_WIDTH + (grid_margin * 0));
            var h = elmObj.height() - (WIDGET_HEIGHT + (grid_margin * 0));
            for (var grid_w = 1; w > 0; w -= (WIDGET_WIDTH + (grid_margin * 0))) {
                grid_w++;
            }
            for (var grid_h = 1; h > 0; h -= (WIDGET_HEIGHT + (grid_margin * 0))) {
                grid_h++;
            }
            gridster.resize_widget(elmObj, parseInt(grid_w), grid_h);
            gridster.set_dom_grid_height(631);
            gridster.serialize();
        }

        //Used for generating GUIDs
        function S4() {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
        }

        //Used for generating GUIDs
        function guid() {
            return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
        }

        //Check if widget height needs to be increased as the user types/pastes content into it
        function checkForResize() {
            setTimeout(function () {
                var computedEditorHeight = 0;
                var someContentFound = false;
                $('#' + ACTIVE_WIDGET_ID + '-widget-content_ifr').contents().children().find('body p').each(function () {
                    computedEditorHeight += $(this).height() + 10;
                    someContentFound = true;
                    //console.log('attempting to resize photo');
                    //$(this).find('img').attr('style', 'max-width: ' + parseInt($('#' + ACTIVE_WIDGET_ID).attr('data-sizex')) * WIDGET_WIDTH + 'px;').attr('height', 'auto');
                });
                if (Math.ceil(computedEditorHeight / WIDGET_HEIGHT) !== parseInt($('#' + ACTIVE_WIDGET_ID).attr('data-sizey'))) {
                    var newWidthMultiplier = parseInt($('#' + ACTIVE_WIDGET_ID).attr('data-sizex'));
                    var newHeightMultiplier = Math.ceil(computedEditorHeight / WIDGET_HEIGHT);

                    if (newHeightMultiplier !== 1) {
                        gridster.resize_widget($('#' + ACTIVE_WIDGET_ID), newWidthMultiplier, newHeightMultiplier);
                        gridster.serialize();
                        $('#' + ACTIVE_WIDGET_ID + '-widget-content_ifr').height(WIDGET_HEIGHT * newHeightMultiplier);
                        $('#' + ACTIVE_WIDGET_ID + '-widget-content_tbl').height(WIDGET_HEIGHT * newHeightMultiplier);
                        $('#' + ACTIVE_WIDGET_ID).height(WIDGET_HEIGHT * newHeightMultiplier);
                    } else {
                        //Check if BOGUS then resize to 2 else resize to 1
                        if (parseInt($('#' + ACTIVE_WIDGET_ID + '-widget-content_ifr').contents().children().find('body p br').attr('data-mce-bogus')) === 1) {
                            //Resize to 2
                            gridster.resize_widget($('#' + ACTIVE_WIDGET_ID), newWidthMultiplier, 2);
                            gridster.serialize();
                            $('#' + ACTIVE_WIDGET_ID + '-widget-content_ifr').height(WIDGET_HEIGHT * 2);
                            $('#' + ACTIVE_WIDGET_ID + '-widget-content_tbl').height(WIDGET_HEIGHT * 2);
                            $('#' + ACTIVE_WIDGET_ID).height(WIDGET_HEIGHT * 2);
                        } else {
                            //Resize to 1
                            //console.log('SMALL INDEED!');
                            //gridster.resize_widget($('#' + ACTIVE_WIDGET_ID), newWidthMultiplier, 1);
                            //gridster.serialize();
                            //$('#' + ACTIVE_WIDGET_ID + '-widget-content_ifr').height(WIDGET_HEIGHT);
                            //$('#' + ACTIVE_WIDGET_ID).height(WIDGET_HEIGHT);
                        }
                    }

                }
            }, 500);    //For a better UX
        }

        //#region Attach widget options
        function attachWidgetOptions(gridsterLI) {
            var selector;
            if (!gridsterLI) {
                selector = '';
                reSizeableSelector = '.widgetx';
                $('.gridster').contents().find('li').append(HTML_WidgetContent).append(HTML_WidgetOptions);
            } else {
                selector = '#' + gridsterLI;
                reSizeableSelector = '#' + gridsterLI;
                $(selector).append(HTML_WidgetContent).append(HTML_WidgetOptions);
            }
            //#region Show/Hide settings cog on hover in/out
            $('.gridster li').mouseenter(function () {
                $(this).children().find('.widget-options').fadeIn(500);
            }).mouseleave(function () {
                if (!widgetOptionsToggleState[$(this).attr('id')]) {
                    //Only hide if the cog hasn't been clicked since if it's clicked we want it to persist as it has siblings
                    $(this).children().find('.widget-options').fadeOut(500);
                }
            });
            //#endregion
            //#region Show/Hide 'edit' and 'delete' on click
            $(selector + ' .widget-options').toggle(function () {
                widgetOptionsToggleState[$(this).parent().parent().attr('id')] = true;
                $(this).css('display', 'block');
                //$(this).siblings('.widget-options-edit').show('slide', { direction: 'right' }, 500).css('display', 'block');
                $(this).siblings('.widget-options-delete').show('slide', { direction: 'right' }, 500).css('display', 'block');
            }, function () {
                widgetOptionsToggleState[$(this).parent().parent().attr('id')] = false;
                //$(this).siblings('.widget-options-edit').hide('slide', { direction: 'right' }, 500);
                $(this).siblings('.widget-options-delete').hide('slide', { direction: 'right' }, 500);
            });
            //#endregion
            //#region Make each widget re-sizable
            $(reSizeableSelector).resizable({
                //grid: [WIDGET_WIDTH + (grid_margin * 2), WIDGET_HEIGHT + (grid_margin * 2)],
                grid: [WIDGET_WIDTH, WIDGET_HEIGHT],
                animate: false,
                handles: 'e',
                minWidth: WIDGET_WIDTH,
                minHeight: WIDGET_HEIGHT,
                containment: '.gridster ul',
                autoHide: true,
                stop: function (event, ui) {
                    var resized = $(this);
                    setTimeout(function () {
                        resizeBlock(resized);
                    }, 400);
                }
            });
            //#endregion
            //#region Make each widget droppable
            $(selector + ', .widgetx').droppable({
                accept: ".draggable-custom-form-item",
                activeClass: "activate-listening-bucket",
                drop: function (event, ui) {
                    //$(this).addClass('dropped-into-listening-bucket');
                    $(this).find('.widget-content')
                        .html(HTML_InjectElement[GLOBAL_DRAGGED_ITEM_TYPE.type].replace('<label></label>', GLOBAL_DRAGGED_ITEM_TYPE.name));
                    switch (GLOBAL_DRAGGED_ITEM_TYPE.type) {
                        case 'checkbox':
                            {
                                $(this).attr('data-elementtype', 'checkbox');
                                $(this).attr('data-elementtypeid', 'checkboxTypeID-' + GLOBAL_DRAGGED_ITEM_TYPE.id);
                                $(this).find('input[type="checkbox"]').uniform();
                                break;
                            }
                        case 'textbox':
                            {
                                $(this).attr('data-elementtype', 'textbox');
                                $(this).attr('data-elementtypeid', 'textboxTypeID-' + GLOBAL_DRAGGED_ITEM_TYPE.id);
                                break;
                            }
                        case 'dropdown':
                            {
                                $(this).attr('data-elementtype', 'dropdown');
                                $(this).attr('data-elementtypeid', 'dropdownTypeID-' + GLOBAL_DRAGGED_ITEM_TYPE.id);
                                $(this).find('select').uniform();
                                break;
                            }
                        case 'datefield':
                            {
                                $(this).attr('data-elementtype', 'datefield');
                                $(this).attr('data-elementtypeid', 'datefieldTypeID-' + GLOBAL_DRAGGED_ITEM_TYPE.id);
                                $(this).find('.datepicker').datepicker({
                                    defaultDate: new Date(),
                                    showButtonPanel: false,
                                    autoSize: true,
                                    changeMonth: true,
                                    changeYear: true,
                                    //appendText: '(mm/dd/yyyy)',
                                    dateFormat: 'mm/dd/yy'
                                });
                                break;
                            }
                        case 'personnel':
                            {
                                $(this).attr('data-elementtype', 'personnel');
                                $(this).attr('data-elementtypeid', 'personnelTypeID-' + GLOBAL_DRAGGED_ITEM_TYPE.id);
                                break;
                            }
                    }
                }
            });
            //#endregion
            //#region Kill gridster temporarily when resizing a widget
            $(selector + ' .ui-resizable-handle').hover(function () {
                gridster.disable();
            }, function () {
                gridster.enable();
            });
            //#endregion
            //#region Remove widget if 'widget-options-delete' is clicked
            $(selector + ' .widget-options-delete').click(function () {
                var whichWidget = $(this).parent().parent();
                $('.widget-delete-dialog').fadeIn(500).dialog({
                    resizable: false,
                    closeOnEscape: false,
                    draggable: false,
                    title: 'Confirm removing content block',
                    position: { my: "top left", at: "top left", of: whichWidget },
                    //open: function(event, ui) { $(".ui-dialog-titlebar-close", ui.dialog || ui).hide(); },
                    height: 140,
                    modal: true,
                    buttons: {
                        Yes: function () {
                            $(this).fadeOut(1000).dialog("close");
                            gridster.remove_widget(whichWidget);
                        },
                        Cancel: function () {
                            $(this).dialog("close");
                        }
                    }
                });
            });
            //#endregion
        }
        //#endregion
        var loadedSerializedForm = {};
        setTimeout(function () {
            WIDGET_WIDTH = Math.ceil(($('.gridster').width() / 6) - 2);
            if (gridster) { gridster = {}; }
            //Launch add-content-block butotn
            $('.footer-poppable-button:eq(1)').show('slide', { direction: 'down' }, 500);
            //Adding new content-text widget
            $('.content-add-text').click(function () {
                var newID = guid();
                gridster.add_widget('<li id="' + newID + '" class="widgetx" data-type="CONTENT" data-elementtype="notset" data-elementtypeid=""></li>', 3, 2, 1, 1);
                attachWidgetOptions(newID);
            });
            //#region LOAD EXISTING FORM FROM DATABASE
            var formID = getParameterByName('formID');
            if (parseInt(formID) > 0) {
                $.ajax({
                    url: 'GetCustomForm',
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({ formID: formID }),
                    success: function (data_received) {
                        loadedSerializedForm = JSON.parse(data_received.serializedForm);
                        $('#formName').val(data_received.formName);
                        $('.gridster').html('<ul></ul>');
                        gridster = $(".gridster ul").gridster({
                            widget_margins: [grid_margin, grid_margin],
                            widget_base_dimensions: [WIDGET_WIDTH, WIDGET_HEIGHT],
                            min_cols: 1,
                            max_size_x: 6,
                            max_size_y: 2,
                            draggable: {
                                stop: function (event, ui) {
                                    gridster.set_dom_grid_height(632);
                                    gridster.serialize();
                                }
                            }
                        }).data('gridster');

                        $.each(loadedSerializedForm, function () {
                            var instance = loadedSerializedForm;
                            var newID = guid();
                            gridster.add_widget('<li id="' + newID + '" class="widgetx" data-type="CONTENT"></li>', this.size_x, this.size_y, this.col, this.row, this.elementtype, this.elementtypeid);
                            attachWidgetOptions(newID);
                            //Get element title
                            var elementTitle;
                            $.ajax({
                                url: 'GetFormElementTitle',
                                type: 'POST',
                                async: false,
                                dataType: 'json',
                                contentType: 'application/json; charset=utf-8',
                                data: JSON.stringify({ elementType: this.elementtype, elementTypeID: this.elementtypeid.replace(/\D/g, '') }),
                                success: function (data_received) {
                                    elementTitle = data_received;
                                }
                            });
                            //Populate each widget with HTML input elements
                            if (this.elementtype !== 'notset') {
                                $('#' + newID).find('.widget-content')
                                    .html(HTML_InjectElement[this.elementtype].replace('<label></label>', '<label>' + elementTitle + '</label>'));
                            }
                            //#region SWITCH
                            switch (this.elementtype) {
                                case 'checkbox':
                                    {
                                        //$(this).attr('data-elementtype', 'checkbox');
                                        //$(this).attr('data-elementtypeid', 'checkboxTypeID-' + GLOBAL_DRAGGED_ITEM_TYPE.id);
                                        $('#' + newID).find('input[type="checkbox"]').uniform();
                                        break;
                                    }
                                case 'textbox':
                                    {
                                        //$(this).attr('data-elementtype', 'textbox');
                                        //$(this).attr('data-elementtypeid', 'textboxTypeID-' + GLOBAL_DRAGGED_ITEM_TYPE.id);
                                        break;
                                    }
                                case 'dropdown':
                                    {
                                        //$(this).attr('data-elementtype', 'dropdown');
                                        //$(this).attr('data-elementtypeid', 'dropdownTypeID-' + GLOBAL_DRAGGED_ITEM_TYPE.id);
                                        $('#' + newID).find('select').uniform();
                                        break;
                                    }
                                case 'datefield':
                                    {
                                        //$(this).attr('data-elementtype', 'datefield');
                                        //$(this).attr('data-elementtypeid', 'datefieldTypeID-' + GLOBAL_DRAGGED_ITEM_TYPE.id);
                                        $('#' + newID).find('.datepicker').datepicker({
                                            defaultDate: new Date(),
                                            showButtonPanel: false,
                                            autoSize: true,
                                            changeMonth: true,
                                            changeYear: true,
                                            //appendText: '(mm/dd/yyyy)',
                                            dateFormat: 'mm/dd/yy'
                                        });
                                        break;
                                    }
                                case 'personnel':
                                    {
                                        break;
                                    }
                            }
                            //#endregion
                        });
                        $('.gridster').css('border', '1px dashed rgba(102, 102, 102, 0.52)');
                        $('.gridster').children().find('.widget-options').fadeOut(1500);
                    }
                });
            }
            //#endregion
        }, 100);

        //#region When a poppable-footer-button is clicked
        $('.footer-poppable-button a').click(function () {
            if ($(this).parent().siblings().children('div').hasClass('footer-button-popup-enable')) {
                $(this).parent().siblings().children('div .footer-button-popup-enable').slideUp(500, function () {
                    $(this).removeClass('footer-button-popup-enable');  //Close the popups that are open
                });
            }
            if ($(this).children('div').hasClass('footer-button-popup-enable')) {
                $(this).siblings('div').slideUp(500, function () { $(this).removeClass('footer-button-popup-enable'); });  //Close this popup
            } else {
                $(this).siblings('div').addClass('footer-button-popup-enable').slideDown(500);  //Open this popup
            }
        });
        //#endregion

        //#region When a footer popup-close button is clicked
        $('.footer-popup-close').click(function () {
            $(this).parent().parent().slideUp(500, function () { $(this).removeClass('footer-button-popup-enable'); });
        });
        //#endregion 

        //#region When the save changes button is clicked
        $('#sticky-footer-save-changes').click(function () {
            if ($('#formName').val().length > 0) {
                var serializedForm = gridster.serialize();
                var formID = getParameterByName('formID');
                if (parseInt(formID) > 0) {
                    //UPDATE EXISTING
                    $.ajax({
                        url: 'UpdateCustomForm',
                        type: 'POST',
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify({ formID: formID, formName: $('#formName').val(), serializedForm: JSON.stringify(serializedForm) }),
                        success: function (data_received) {
                            Notifications.displayGlobalNotification(data_received, function () {
                                if (data_received.type == 3) {
                                    //
                                }
                            });
                        }
                    });
                } else {
                    //ADD NEW
                    $.ajax({
                        url: 'AddCustomForm',
                        type: 'POST',
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify({ formName: $('#formName').val(), formType: $('#formType').val(), serializedForm: JSON.stringify(serializedForm) }),
                        success: function (data_received) {
                            Notifications.displayGlobalNotification(data_received, function () {
                                if (data_received.type == 3) {
                                    //
                                }
                            });
                        }
                    });
                }
            }
        });
        //#endregion

        //Adding new photo-slider widget
        //$('.content-add-slider').click(function () {
        //    var newID = guid();
        //    gridster.add_widget('<li id="' + newID + '" class="widgetx" data-type="SLIDER"></li>', 3, 2, 1, 1);
        //    attachWidgetOptions(newID);
        //});

        //When a layout type is picked from the footer
        $('.footer-layout-pick-option').click(function () {
            //Load layout HTML
            var whichLayout = $(this).attr('data-layout');
            $('.gridster').html(HTML_Layout[whichLayout]);
            attachWidgetOptions();
            //Let it be known I exist
            $('.gridster').children().find('.widget-options').fadeOut(1500);
            //Initialize gridster
            if (gridster) { gridster = {}; }
            gridster = $(".gridster ul").gridster({
                widget_margins: [grid_margin, grid_margin],
                widget_base_dimensions: [WIDGET_WIDTH, WIDGET_HEIGHT],
                min_cols: 1,
                min_rows: 2,
                max_size_x: 6,
                draggable: {
                    stop: function (event, ui) {
                        gridster.set_dom_grid_height(632);
                        gridster.serialize();
                    }
                }
            }).data('gridster');
            $('.gridster').css('border', '1px dashed rgba(102, 102, 102, 0.52)');
        });
    });
}(window.skillet = window.skillet || {}, jQuery));