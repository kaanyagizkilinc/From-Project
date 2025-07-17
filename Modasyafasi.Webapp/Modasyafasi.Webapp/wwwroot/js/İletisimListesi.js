document.addEventListener('DOMContentLoaded', function () {

    dataTAbleRsrt();

    function dataTAbleRsrt() {
        $.ajax({
            url: '/Home/IletisimListesGET',
            type: 'GET',
            dataType: 'json',
            success: function (kullanicilar) {
                console.log("Kullanıcı verisi:", kullanicilar);
                MedyaListele(kullanicilar);
            },
            error: function (xhr, status, error) {
                console.error("Kullanıcı verisi alınamadı:", error);
            }
        });
    }

    function MedyaListele(kullanicilar) {
        $.ajax({
            url: '/Home/KonumVeriAl',
            type: 'GET',
            dataType: 'json',
            success: function (konumlar) {
                console.log("Konum verisi:", konumlar);

                var NameOfData = kullanicilar.map(function (item) {
                    item.adSoyad = item.ad + ' ' + item.soyad;

                    let MedyaHtml = '<div style="display:flex; width: 60px";>';
                    if (item.medivm && item.medivm.length > 0) {
                        item.medivm.forEach(function (medya) {
                            MedyaHtml += `
                                <div style="text-align:center;">
                                    <img src="/MediaKutuphane/${medya.mediaUrl}" alt="${medya.mediaAdı}" width="60" style="border-radius:4px; margin-left: 10px;" />
                                </div>
                            `;
                        });
                        MedyaHtml += '</div>';
                    } else {
                        MedyaHtml = '<span>Medya bulunamadı</span>';
                    }

                    item.MedyaHtml = MedyaHtml;

                    if (item.cinsiyet === "0") {
                        item.cinsiyet = "Kadın";
                    } else if (item.cinsiyet === "1") {
                        item.cinsiyet = "Erkek";
                    } else {
                        console.warn("hatalı veri cinsiyet:", item.cinsiyet);
                        item.cinsiyet = "Bilinmiyor";
                    }

                    var ulke = konumlar.find(k => k.id == item.ulke && k.ustid == 0);
                    var sehir = konumlar.find(k => k.id == item.sehir && k.ustid != 0);

                    var ulkeAdi = ulke ? ulke.tanim : "Bilinmiyor";
                    var sehirAdi = sehir ? sehir.tanim : "Bilinmiyor";
                    item.ulkeAdi = ulkeAdi + " / " + sehirAdi;

                    return item;
                });

                if ($.fn.DataTable.isDataTable('#datatable')) {
                    $('#datatable').DataTable().clear().destroy();
                }

                $("#datatable").DataTable({
                    data: NameOfData,
                    columns: [
                        {
                            data: 'MedyaHtml',
                            title: 'Pp',
                            orderable: false,
                            searchable: false,
                            render: function (data) {
                                return data;
                            }
                        },
                        { data: 'adSoyad', title: 'Ad-Soyad' },
                        { data: 'dogumTarihi', title: 'Doğum Tarihi' },
                        { data: 'cinsiyet', title: 'Cinsiyet' },
                        { data: 'ulkeAdi', title: 'Konum' },
                        { data: 'aciklama', title: 'Açıklama' },
                        {
                            data: null,
                            title: 'İşlemler',
                            orderable: false,
                            searchable: false,
                            render: function (data, type, row) {
                                return '<button style="margin-top:20px;" class="btn btn-warning" onclick="window.location.href=\'/Home/ListeGuncelle?id=' + row.id + '\'">Güncelle</button> ' +
                                    '<button style="margin-top:20px;" class="btn btn-danger" onclick="Sil(' + row.id + ')">Sil</button>';
                            }
                        }
                    ],
                    order: [[1, 'asc']],
                    scrollX: true,
                    autoWidth: false
                });
            },
            error: function (xhr, status, error) {
                console.error("Konum verisi alınamadı:", error);
            }
        });
    }
});
