
function Sil(id) {
    Swal.fire({
        title: 'Emin misiniz?',
        text: 'Bu veri silinmek üzere!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Evet, sil!',
        cancelButtonText: 'Vazgeç'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Home/VeriSil',
                type: 'POST',
                dataType: 'json',
                data: { id: id },
                success: function () {
                    Swal.fire(
                        'Silindi!',
                        'Veri Başarıyla silindi',
                        'success'
                    );


                    var table = $("#datatable").DataTable();
                    table.rows(function (idx, data, node) {
                        return data.id == id ? true : false;

                    }).remove().draw(false);
                    setTimeout(() => location.reload(), 1500);
                },
                error: function () {
                    Swal.fire(
                        'Hata!',
                        'Silme işlemi sırasında bir sorun oluştu.',
                        'error'
                    );
                }
            });
        } else {
            Swal.fire(
                'İptal Edildi',
                'Veri güvende 👌',
                'info',
            );
        }
    });

}