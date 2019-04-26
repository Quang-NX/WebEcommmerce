var homeController = {
    init: function () {
        homeController.registerEvent();
        homeController.loadData();
    },
    registerEvent: function () {

    },
    loadData: function () {
        $.ajax({
            url: '/Admin/SetRole/LoadData',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $("#data-template").html();
                    $.each(data, function (key, item) {
                        html += Mustache.render(template, {
                            Id: item.Id,
                            UserName: item.UserName,
                            Age: item.Age,
                            Email: item.Email,
                            Address: item.Address,
                            PhoneNumber: item.PhoneNumber,
                            RoleId: item.Roles.Id,
                            RoleName:item.Roles.Name
                        })
                    });
                    $("#tblData").html(html);
                }
            }
        })
    }
}
homeController.init();