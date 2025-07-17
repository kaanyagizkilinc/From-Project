function KullaniciGetir(id) {
    $.ajax({
        url: '/Home/KullaniciGetir?id=' + id,
        type: 'GET',
        dataType: 'json',
        success: function (data) {

            $("#Id").val(data.id);
            $("#Ad").val(data.ad);
            $("#Soyad").val(data.soyad);
            $("#DogumTarihi").val(data.dogumTarihi ? data.dogumTarihi.substring(0, 10) : '');
            $("#Ulke").val(data.ulke);
            $("#Sehir").val(data.sehir);
            $("#Aciklama").val(data.aciklama);

            if (data.cinsiyet === "1") {
                $("#erkekcheck").prop("checked", true);
                $("#kadincheck").prop("checked", false);
            } else if (data.cinsiyet === "0") {
                $("#kadincheck").prop("checked", true);
                $("#erkekcheck").prop("checked", false);
            } else {
                $("#erkekcheck").prop("checked", false);
                $("#kadincheck").prop("checked", false);
            }

            console.log("Kullanıcı verileri form dolduruldu:", data);
        },
        error: function (xhr) {
            alert("Kullanıcı bilgileri alınamadı: " + xhr.status);
        }
    });
}