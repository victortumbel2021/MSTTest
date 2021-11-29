
var AddEditKodePosIndonesia = function (id) {
    var url = "/KodePosIndonesia/AddEditKodePosIndonesia?id=" + id;
    if (id > 0)
        $('#title').html("Edit Kode Pos");

    $("#KodePosIndonesiaFormModelDiv").load(url, function () {
        $("#KodePosIndonesiaFormModel").modal("show");

    });
};

$('body').on('click', "#btnSubmit", function () {
    var myformdata = $("#KodePosIndonesiaForm").serialize();

    if ($.trim($('#KodePos').val()) === '') {
        Swal.fire({
            title: "Alert", text: "Kode Pos can not be left blank.",
            icon: "info", closeOnConfirm: false,
            onAfterClose: () => {
                setTimeout(() => $("#KodePos").focus(), 110);
            }
        });
        return;
    }
    if ($.trim($('#Kelurahan').val()) === '') {
        Swal.fire({
            title: "Alert", text: "Kelurahan can not be left blank.",
            icon: "info", closeOnConfirm: false,
            onAfterClose: () => {
                setTimeout(() => $("#Kelurahan").focus(), 110);
            }
        });
        return;
    }

    if ($.trim($('#Kecamatan').val()) === '') {
        Swal.fire({
            title: "Alert", text: "Kecamatan can not be left blank.",
            icon: "info", closeOnConfirm: false,
            onAfterClose: () => {
                setTimeout(() => $("#Kecamatan").focus(), 110);
            }
        });
        return;
    }

    if ($.trim($('#Jenis').val()) === '') {
        Swal.fire({
            title: "Alert", text: "Jenis can not be left blank.",
            icon: "info", closeOnConfirm: false,
            onAfterClose: () => {
                setTimeout(() => $("#Jenis").focus(), 110);
            }
        });
        return;
    }

    if ($.trim($('#Kabupaten').val()) === '') {
        Swal.fire({
            title: "Alert", text: "Kabupaten can not be left blank.",
            icon: "info", closeOnConfirm: false,
            onAfterClose: () => {
                setTimeout(() => $("#Kabupaten").focus(), 110);
            }
        });
        return;
    }

    if ($.trim($('#Propinsi').val()) === '') {
        Swal.fire({
            title: "Alert", text: "Propinsi can not be left blank.",
            icon: "info", closeOnConfirm: false,
            onAfterClose: () => {
                setTimeout(() => $("#Propinsi").focus(), 110);
            }
        });
        return;
    }

    $.ajax({
        type: "POST",
        url: "/KodePosIndonesia/Create",
        data: myformdata,
        success: function (result) {
            $("#KodePosIndonesiaFormModel").modal("hide");

            Swal.fire({
                title: "Alert!",
                text: result,
                type: "Success"
            }).then(function () {
                $('#tblKodePosIndonesia').DataTable().ajax.reload();
            });
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
});

var DeleteKodePosIndonesia = function (id) {
    Swal.fire({
        title: 'Do you want to delete this item?',
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: "/KodePosIndonesia/Delete/" + id,
                success: function () {
                    Swal.fire({
                        title: 'Deleted!', text: 'Kode Pos has been deleted.',
                        icon: "success", closeOnConfirm: false,
                        onAfterClose: () => {
                            $('#tblKodePosIndonesia').DataTable().ajax.reload();
                        }
                    });
                }
            });
        }
    });
};



