var YuklenenMedyaKayit = [];

document.gElementById('mediaInput').addEventListener('change', function (event) {
    YuklenenMedyaKayit = Array.from(event.target.files);
    console.log("Yüklenen medya dosyaları:", YuklenenMedyaKayit);
});


if(YuklenenMedyaKayit.length === 0) {
    Swal.fire({
        icon: 'warning',
        title: 'Uyarı!',
        text: 'Lütfen en az bir medya dosyası yükleyin.',
        confirmButtonText: 'Tamam'
    });
    return;
}

var FormMediaData = new FormData();
YuklenenMedyaKayit.forEach((file, index) => {
    FormMediaData.append(`mediaFiles[${index}]`, file);
});

$.ajax({
    url: '/Home/MediaKayit',
    data: FormMediaData,
    type: 'POST',
    contentType: false,
    processData: false,
    success: function (response) {
        console.log("Medya kaydı başarılı:", response);
    },
    error: function (xhr, status, error) {
        console.error("Medya kaydı sırasında hata:", error);
    }
});