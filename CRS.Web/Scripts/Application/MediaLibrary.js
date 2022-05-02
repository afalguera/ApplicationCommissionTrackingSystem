var viewModel = function () {
    var self = this;
    var modes = { Add: 0, Edit: 1, View: 2 };
   
    $('body').on('show', '.modal', function () {
        $(this).css({ 'width': '500px', 'margin-top': ($(window).height() - $(this).height()) / 2, 'top': '0' });
    });

    self.items = ko.observableArray();
    self.mode = ko.observable(modes.View);
    self.itemColumns = ['FileName', 'FileType', 'FileSize'];

    self.isEditMode = ko.computed(function () {
        return self.mode() == modes.Edit;
    });
    self.isAddMode = ko.computed(function () {
        return self.mode() == modes.Add;
    });

    self.title = ko.computed(function () {
        return self.isEditMode() ? 'Edit Media Library' : 'Add Media Library';
    });

    self.id = ko.observable();
    self.strMessage = ko.observable();
  
    function getItems() {
        $.ajax({
            url: '/MediaLibrary/GetMediaLibraryList',
            type: 'GET',
            data: {},
            beforeSend: function () {
                $.blockUI({ baseZ: 2000, message: '<h4><img src="../Content/images/ajax-loader.gif" /><br/> Just a moment...</h4>' });
            },
            complete: function () {
                $.unblockUI();
            },
            success: function (data) {
                self.items(data);
            },
            error: function () {
                $.unblockUI();
            }
        });
    };

  
    //add
    self.add = function () {
        self.mode(modes.Add);        
    }

    //delete
    self.deleteMediaLibrary = function () {
        $.ajax({
            url: '/MediaLibrary/DeleteMediaLibrary',
            type: 'DELETE',
            data: {
                imgPath: self.id()
            },
            beforeSend: function () {
                $.blockUI({ baseZ: 2000, message: '<h4><img src="../Content/images/ajax-loader.gif" /><br/> Just a moment...</h4>' });
            },
            complete: function () {
                $.unblockUI();
            },
            success: function (data) {
                getItems();
                $('#confirmDeleteModal').modal('hide');
            },
            error: function () {
                $.ambiance({
                    message: 'Error encountered',
                    type: "error",
                    title: "Error Notification!",
                    fade: false
                });
                $.unblockUI();
            }
        });
    };

    //set Id  
    self.setId = function (data) {
        self.id(data.FilePath);
    }

    //save
    self.save = function () {
     
        if (self.isValidFile()) {       
            $("#form1").submit();
        } else {
            var str = self.strMessage();
            if (!isWhitespaceNotEmpty(str)) {
                $.ambiance({
                    message: str,
                    type: "error",
                    title: "Error Notification!",
                    fade: false
                });
            }
            return false;
        }
    }

    //-- check if string if null, empty or whitespace
    var isWhitespaceNotEmpty = function (str) {
        return (!str || 0 === str.length) && (!str || /^\s*$/.test(str));
    }
    //---------------------------------- end

    self.isValidFile = function () {
        var file1 = $('#file1').val();
        var strMessage = '';
        if (file1 == '' || typeof file1 == 'undefined') {
            strMessage = "File is required.";
            self.strMessage(strMessage);
            return false;
        }
          return true;
    };
  
    getItems();
}

$(function () {
    ko.applyBindings(new viewModel());
    $(".colorButton").removeAttr('style').css("width", "100px");
});