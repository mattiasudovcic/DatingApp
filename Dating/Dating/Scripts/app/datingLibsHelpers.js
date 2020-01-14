$.datingLibs_setDatePicker = function (control, controlOnSelect = null, minDate = null, maxDate = null, actualDate = null, daysToAdd = 0, $successAfterSelect, showOnFocus = true) {

    var showOn = false;
    if (showOnFocus) {
        showOn = "focus";
    }
    $(control).datepicker({
        format: "dd/mm/yyyy",
        minDate: minDate,
        maxDate: maxDate,
        autoclose: true,
        prevText: '<i class="fa fa-chevron-left"></i>',
        nextText: '<i class="fa fa-chevron-right"></i>',
        language: 'es',
        changeYear: true,
        changeMonth: true,
        showOn: showOn,
        onSelect: function (selectedDate) {

            if (controlOnSelect != null) {
                var dateParts = selectedDate.split("/");
                var dateObject = new Date(+dateParts[2], dateParts[1] - 1, +dateParts[0]);
                dateObject.setDate(dateObject.getDate() + daysToAdd);
                $(controlOnSelect).datepicker('option', 'minDate', dateObject);

            }

            if ($successAfterSelect != null)
                $successAfterSelect();
        }
    });

    if (actualDate != null) {

        $(control).datepicker('setDate', actualDate);
    }
}


$.datingLibs_disableControl = function (control, disabled) {
    if (disabled) {

        $(control).attr("disabled", "disabled");
        $(control).addClass('input-disabled');
    } else {

        $(control).removeAttr("disabled");
        $(control).removeClass('input-disabled');
    }
}



$.datingLibs_fillListCheckBox = function (control, actionURL, validateItemToAddInDualListMethod, validateSelectedItem, parentId = null, onSuccessFunction = null) {
    
    $.ajax({
        url: actionURL,
        dataType: "json",
        data: { parentId: parentId },
        type: "POST",
        error: function () {
            console.log("Error");
        },
        success: function (data) {
          
            $(control).empty();
            $.each(data.Options, function (i, obj) {
                
                var selectedItem = validateSelectedItem && validateSelectedItem(obj.Value);
                var itemHtml;
                if (selectedItem)
                    itemHtml = '<div class=""><label><input type="checkbox" class="checkbox vcheck" value=' + obj.Value + ' checked="checked"><span> ' + obj.DisplayText + ' </span></label></div>';
                else
                    itemHtml = '<div class=""><label><input type="checkbox" class="checkbox vcheck" value=' + obj.Value + ' ><span> ' + obj.DisplayText + ' </span></label></div>';
                if (validateItemToAddInDualListMethod)
                    if (validateItemToAddInDualListMethod(obj.Value) == false)  //Saltea el elemento.
                        return true;
                $(control).append($(itemHtml));
            });

            if (onSuccessFunction != null) {
                onSuccessFunction();
            }
            
        }
    });
}



$.datingLibs_executeAjax = function (url, type, data, contentType, processData, $successCallback, $getPostDataCustom) {
    
    var formData = "";

    if ($getPostDataCustom != null && $getPostDataCustom != undefined) {
        formData = $getPostDataCustom();
    }
    else {
        formData = data;
    }

    $.ajax({
        url: url,
        type: type,
        data: formData,
        processData: processData,
        contentType: contentType,
        success: function (data, textStatus, jqXHR) {          

            if ($successCallback != null && $successCallback != undefined) {
                $successCallback(data);
            }
            
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log("Error");
        }
    }).then(function () {
        console.log("Then");
    });
};


$.datingLibs_showModal = function (ajaxContainerId) {
   
    $("html").addClass('scrolling-fixed');
    $(ajaxContainerId).attr("data-backdrop", "static");
    $(ajaxContainerId).css("overflow-y", "auto");
    $(ajaxContainerId).modal({
        show: true,
        keyboard: true,
        backdrop: 'static'
    });
}

