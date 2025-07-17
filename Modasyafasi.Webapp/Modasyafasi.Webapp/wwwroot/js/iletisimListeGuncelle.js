
//var SilinenOkulDiv = [];//Çalışıyor
//var EklenenOkulDiv = [];
//var SilinenMedya = [];//Çalışıyor
//var silinecekMedyaIdler = []
//var YeniOkulTutucu = [];
//let counter = 0;
//var kullaniciId;

//document.addEventListener('DOMContentLoaded', function () {
//    var params = new URLSearchParams(window.location.search);
//    var id = params.get("id");
//    kullaniciId = id;
//    const MedyaAlanlari = $('#medyaAlanlari');
//    OkulVerigetir(kullaniciId);//okulId gelicek
//    function OkulVerigetir(kullaniciId) {
//        $.ajax({
//            url: '/Home/OkulVeriGetir?id=' + kullaniciId,
//            type: 'GET',
//            data: { KullaniciId: kullaniciId },
//            dataType: 'json',
//            success: function (data) {
//                console.log(data);
//                const okulListSahte = data[0].okulBilgiVM;
//                if (data[0]?.okulBilgiVM && data[0].okulBilgiVM.length > 0) {
//                    data[0].okulBilgiVM.forEach(item => {
//                        if (item.okulBilgisi && item.okulBilgisi.id) {
//                            YeniOkulTutucu.push(item.okulBilgisi.id);
//                        }
//                    });
//                } else {
//                    YeniOkulTutucu = [];
//                }
//                console.log("Tüm okul idleri:", YeniOkulTutucu);
//                console.log(YeniOkulTutucu);
//                let FakeID = YeniOkulTutucu.length > 0 ? YeniOkulTutucu[YeniOkulTutucu.length - 1] : 0;
//                console.log("FakeID: "+FakeID)
//                const okulBilgiListesi = data[0].okulBilgiVM;


//                //SILME BUTONU
//                document.getElementById('medyaAlanlari').addEventListener('click', function (e) {
//                    if (e.target.classList.contains('sil-btn')) {
//                        e.preventDefault();
//                        const div = e.target.closest('.media-div');
//                        const okulId = div.querySelector('.okul-id-hidden')?.value;
//                        if (okulId) {
//                            SilinenOkulDiv.push(parseInt(okulId));
//                            console.log(SilinenOkulDiv);
//                        }
//                        div.remove();
//                    }
//                });
//                //EKLEME BUTONU
//                document.getElementById('medyaAlanlari').addEventListener('click', function (e) {
//                    if (e.target.classList.contains('ekle-btn')) {
//                        counter++;
//                        console.log("counter :"+counter);
//                        FakeID++;
//                        e.preventDefault();
//                        const div = document.createElement('div');
//                        div.style.boxShadow = "4px 4px 12px black";
//                        div.classList.add('media-div', 'mt-3', 'bg-dark', 'rounded', 'p-2');
//                        div.setAttribute('data-id', `${counter}`);
//                        div.innerHTML = `
//                                <h5 class="text-light">Mezuniyet Bilgisi #${counter}</h5>
//                                <input type="hidden" class="okul-id-hidden" value="${FakeID}" />

//                                <label class="form-label text-light"><strong>Okul Türü:</strong></label>
//                                <select class="form-control okul-select" id="Okul${counter}">
//                                    <option value="1">Lise</option>
//                                    <option value="2">Üniversite</option>
//                                </select>

//                                <label class="form-label text-light mt-2"><strong>Mezuniyet Tarihi:</strong></label>
//                                <input type="date" id="MezuniyetYili${counter}" class="form-control mezuniyet-tarihi">

//                                <label class="form-label text-light mt-2"><strong>Okul Adı:</strong></label>
//                                <input type="text" class="form-control okul-adi" id="OkulAdi${counter}" placeholder="Okul Adı giriniz...">

//                                <label class="form-label text-light mt-2"><strong>Diploma:</strong></label>
//                                <input type="file" class="form-control media-input" id="mediaIn${counter}" name="Diploma" multiple>

//                                <div id="diplomaPreview${counter}" class="d-flex flex-wrap mt-2"></div>
//                                <button type="button" class="btn btn-danger mt-2 sil-btn">-</button>
//                                <button type="button" class="btn btn-success mt-2 ekle-btn">+</button>
//                            `;
//                        MedyaAlanlari.append(div);
//                        YeniOkulTutucu.push(parseInt(FakeID));
//                        EklenenOkulDiv.push(parseInt(FakeID));
//                        console.log(YeniOkulTutucu);
//                        console.log(EklenenOkulDiv);
//                    }
//                });
//                if (!data || data.length === 0) {
//                    console.warn("Okul verisi boş veya gelmedi.");
//                    return;
//                }



//                if (!okulBilgiListesi) {
//                    console.warn("OkulBilgiVM undefined.");
//                    return;
//                }


//                MedyaAlanlari.on('change', '.media-input', function (e) {
//                    const inputId = $(this).attr("id");
//                    const index = inputId.replace("mediaIn", "");
//                    const previewContainer = $(`#diplomaPreview${index}`);

//                    Array.from(this.files).forEach(file => {
//                        const reader = new FileReader();
//                        reader.onload = function (event) {
//                            const img = $(`
//                                <div class="position-relative media-prev-div m-2">
//                                <input type="hidden" class="okul-id-hiddenMedia" value ="0" />
//                                <img src="${event.target.result}" alt="eklenen" 7
//                                style="width:200px; height:200px; border:12px solid black; border-radius:43px;" />
//                                <button class="btn btn-sm btn-danger position-absolute top-0 end-0 medya-sil-btn"
//                                data-media-id="0">&times;</button>
//                                </div>`);
//                            previewContainer.append(img);
//                        };
//                        reader.readAsDataURL(file);
//                    });
//                });


//                okulBilgiListesi.forEach(function (item, index) {
//                    console.log("index :"+index);
//                    const okulBilgisi = item.okulBilgisi;
//                    const medyaListesi = item.media;

//                    const div = $(`
//                    <div class="media-div mt-3 bg-dark rounded p-2" data-id="${index + 1}">
//                        <h5 class="text-light">Mezuniyet Bilgisi #${index + 1}</h5>
//                        <input type="hidden" class="okul-id-hidden" value="${okulBilgisi.id}" />

//                        <label class="form-label text-light"><strong>Okul Türü:</strong></label>
//                        <select class="form-control okul-select" id="Okul${index + 1}">
//                            <option value="1" ${okulBilgisi.okulTuru == 1 ? 'selected' : ''}>Lise</option>
//                            <option value="2" ${okulBilgisi.okulTuru == 2 ? 'selected' : ''}>Üniversite</option>
//                        </select>

//                        <label class="form-label text-light mt-2"><strong>Mezuniyet Tarihi:</strong></label>
//                        <input type="date" id="MezuniyetYili${index + 1}" class="form-control mezuniyet-tarihi" value="${okulBilgisi.mezuniyetYili?.substring(0, 10) || ''}">

//                        <label class="form-label text-light mt-2"><strong>Okul Adı:</strong></label>
//                        <input type="text" class="form-control okul-adi" id="OkulAdi${index + 1}" value="${okulBilgisi.okulAdi}" placeholder="Okul Adı giriniz...">

//                        <label class="form-label text-light mt-2"><strong>Diploma:</strong></label>
//                        <input type="file" class="form-control media-input" id="mediaIn${index + 1}" multiple>

//                        <div id="diplomaPreview${index + 1}" class="d-flex flex-wrap mt-2"></div>
//                                        <button type="button" class="btn btn-danger mt-2 sil-btn">-</button>
//                                        <button type="button" class="btn btn-success mt-2 ekle-btn">+</button>
//                    </div>

//                `);
//                    MedyaAlanlari.append(div);
//                    counter = okulBilgiListesi.length;
//                    const diplomaPreview = $(`#diplomaPreview${index + 1}`);
//                    diplomaPreview.empty();
//                    medyaListesi.forEach(function (media) {
//                        const imgDiv = $(`
//                                <div class="position-relative media-prev-div m-2">
//                                <input type="hidden" class="okul-id-hiddenMedia" value="${media.id}" />
//                                <img src="/MediaKutuphane/${media.mediaUrl}" alt="${media.mediaAdı}" 7
//                                style="width:200px; height:200px; border:12px solid black; border-radius:43px;" />
//                                <button class="btn btn-sm btn-danger position-absolute top-0 end-0 medya-sil-btn"
//                                data-media-id="${media.id}">&times;</button>
//                                </div>
//                            `);
//                        diplomaPreview.append(imgDiv);
//                    });
//                    document.getElementById(`diplomaPreview${index + 1}`).addEventListener('click', function (e) {
//                        if (e.target.classList.contains('medya-sil-btn')) {
//                            e.preventDefault();
//                            const div = e.target.closest('.media-prev-div');
//                            const mediaId = div.querySelector('.okul-id-hiddenMedia')?.value;
//                            if (mediaId == 0) {
//                                div.remove();
//                            }
//                            if (mediaId) {
//                                SilinenMedya.push(parseInt(mediaId));
//                                console.log(SilinenMedya);
//                            }
//                            div.remove();
//                        }
//                    });


//                    document.getElementById("GuncelleBtn").addEventListener("click", Guncelle);


//                });
//            }
//        });
//        function Guncelle() {
//            var cins;
//            var erkek = document.getElementById('erkekcheck').checked;
//            var kadin = document.getElementById('kadincheck').checked;
//            if (erkek) {
//                cins = "1";
//            }
//            else if (kadin) {
//                cins = "0";
//            }
//            else {
//                console.log("hata cinsiyet null:" + cins);
//            }
//            console.log("Cinsiyet:", cins);

//            var ad = $("#Ad").val();
//            var soyad = $("#Soyad").val();
//            var date = $("#DogumTarihi").val();
//            if (!date) {
//                alert("Doğum tarihi boş olamaz!");
//                return;
//            }
//            console.log("Date:", date);
//            var ulke = $("#Ulke").val();
//            var sehir = $("#Sehir").val();
//            var aciklama = $("#Aciklama").val();

//            var model = {
//                Id: kullaniciId,
//                Ad: ad,
//                Soyad: soyad,
//                DogumTarihi: date,
//                Cinsiyet: cins,
//                Ulke: ulke,
//                Sehir: sehir,
//                Aciklama: aciklama
//            };
//            console.log("ajax başlıyor");
//            $.ajax({
//                url: "/Home/KullaniciGuncelle",
//                type: "PUT",
//                contentType: "application/json",
//                data: JSON.stringify(model),
//                success: function (data) {
//                    console.log("Başarılı cevap geldi:");
//                    console.log(data);
//                    console.log(YeniOkulTutucu);
//                    const okulIdInputs = document.querySelectorAll('.okul-id-hidden');
//                    YeniOkulTutucu = Array.from(okulIdInputs).map(input => parseInt(input.value));
//                    for (let i = 0; i < YeniOkulTutucu.length; i++) {


//                        const okulId = YeniOkulTutucu[i];
//                        const okulturuEl = document.getElementById(`Okul${i + 1}`);
//                        const mezuniyetTarihiEl = document.getElementById(`MezuniyetYili${i + 1}`);
//                        const okulAdiEL = document.getElementById(`OkulAdi${i + 1}`);
//                        const diplomaEL = document.getElementById(`mediaIn${i + 1}`);



//                        if (!okulturuEl || !mezuniyetTarihiEl || !okulAdiEL || !diplomaEL) {
//                            console.warn(`Mezuniyet bilgisi #${i} için gerekli alanlar eksik.`);
//                        }
//                        console.log("DiplomaEl files" + diplomaEL);

//                        if (!diplomaEL) {
//                            console.warn(`mediaIn${i + 1} input bulunamadı.`);
//                        }

//                        console.log(diplomaEL)

//                        const okulTuru = okulturuEl.value;
//                        const mezuniyetTarihi = mezuniyetTarihiEl.value;
//                        const okulAdi = okulAdiEL.value;

//                        const KullaniciOkuljson = {
//                            Id: okulId,
//                            KullaniciId: kullaniciId,
//                            OkulTuru: parseInt(okulTuru),
//                            MezuniyetYili: mezuniyetTarihi,
//                            OkulAdi: okulAdi
//                        };

//                        console.log("KullaniciOkuljson:", KullaniciOkuljson);
//                        $.ajax({
//                            url: '/Home/KullaniciOkulGuncelle',
//                            type: 'PUT',
//                            dataType: 'json',
//                            contentType: 'application/json',
//                            data: JSON.stringify(KullaniciOkuljson),
//                            success: function (response) {
//                                console.log("Kullanıcı okul bilgisi güncellendi:", response);
//                                var OkulId = okulId;
//                                const formMediaData = new FormData();
//                                const diplomaFiles = diplomaEL.files;
//                                if (!diplomaFiles || diplomaFiles.length === 0) {
//                                    console.warn("Diploama dosyası boş " + diplomaFiles);
//                                }
//                                console.log("Input:", diplomaEL);
//                                console.log("Dosyalar:", diplomaFiles);

//                                if (!OkulId) {
//                                    console.warn("Okul ID alınamadı, güncelleme yapılamadı.");
//                                    return;
//                                }
//                                for (let i = 0; i < diplomaFiles.length; i++) {
//                                    formMediaData.append('Diploma', diplomaFiles[i]);
//                                }
//                                formMediaData.append('OkulId', OkulId);
//                                formMediaData.append('kullaniciId', kullaniciId);
//                                for (let pair of formMediaData.entries()) {
//                                    if (pair[1] instanceof File) {
//                                        console.log(pair[0], pair[1].name); // Dosya ismini yazdır
//                                    } else {
//                                        console.log(pair[0], pair[1]); // Diğer parametreleri yazdır
//                                    }
//                                }
//                                if (diplomaFiles) {
//                                    $.ajax({
//                                        url: '/Home/DiplomaKayit',
//                                        type: 'POST',
//                                        data: formMediaData,
//                                        contentType: false,
//                                        processData: false,
//                                        success: function (response) {
//                                            console.log("Diploma güncelleme başarılı:", response);
//                                        },
//                                        error: function (xhr, status, error) {
//                                            console.error("Diploma güncelleme sırasında hata:", error);
//                                        }
//                                    });
//                                }
//                            },
//                            error: function (xhr, status, error) {
//                                console.error("Kullanıcı okul bilgisi güncellenirken hata:", error);
//                            }
//                        });
//                    }
//                    for (let i = 0; i < SilinenMedya.length; i++) {
//                        console.log("Silinen medya id:", SilinenMedya[i]);
//                        $.ajax({
//                            url: `/Home/SilinenOkulGuncelle/${SilinenMedya[i]}`,
//                            type: 'DELETE',
//                            success: function (response) {

//                                console.log("Silinen okul bilgileri güncellendi:", response);
//                            },
//                            error: function (xhr, status, error) {
//                                console.error("Silinen okul bilgileri güncellenirken hata:", error);
//                            }
//                        });
//                    }
//                    for (let i = 0; i < SilinenOkulDiv.length; i++) {
//                        $.ajax({
//                            url: `/Home/SilinenOkulGuncelleDiv/${SilinenOkulDiv[i]}`,
//                            type: 'DELETE',
//                            success: function (response) {

//                                console.log("Silinen okul bilgileri güncellendi:", response);
//                            },
//                            error: function (xhr, status, error) {
//                                console.error("Silinen okul bilgileri güncellenirken hata:", error);
//                            }
//                        });
//                    }
//                    const mediaInput = document.getElementById(`mediaInput`);
//                    if (mediaInput && mediaInput.files.length > 0) {
//                        const YuklenenMedyaKayit = Array.from(mediaInput.files);
//                        var FormMediaData = new FormData();
//                        YuklenenMedyaKayit.forEach((file) => {
//                            FormMediaData.append(`mediaFiles`, file);
//                        });
//                        FormMediaData.append('Id', data.id);

//                        $.ajax({
//                            url: '/Home/MediaGuncelle',
//                            data: FormMediaData,
//                            type: 'PUT',
//                            contentType: false,
//                            processData: false,
//                            success: function (response) {
//                                console.log("Medya kaydı başarılı:", response);

//                            },
//                            error: function (xhr, status, error) {
//                                console.error("Medya kaydı sırasında hata:", error);
//                            }
//                        });
//                    }
//                    Swal.fire({
//                        icon: 'success',
//                        title: 'Güncelleme Başarılı!',
//                        text: 'Bilgiler başarıyla kaydedildi.',
//                        confirmButtonText: 'Tamam'
//                    });
//                    setTimeout(() => window.location.href = "/Home/iletisimList", 2000);

//                },
//                error: function (xhr) {
//                    Swal.fire({
//                        icon: 'error',
//                        title: 'Hata!',
//                        text: 'Kayıt sırasında bir sorun oluştu.'
//                    });
//                }
//            });
//            console.log(model);
//        }

//    }
//});



var SilinenOkulDiv = [];//Çalışıyor
var EklenenOkulDiv = [];
var SilinenMedya = [];//Çalışıyor
var silinecekMedyaIdler = []
var YeniOkulTutucu = [];
let counter = 0;
var kullaniciId;

document.addEventListener('DOMContentLoaded', function () {
    var params = new URLSearchParams(window.location.search);
    var id = params.get("id");
    kullaniciId = id;
    const MedyaAlanlari = $('#medyaAlanlari');
    OkulVerigetir(kullaniciId);//okulId gelicek
    function OkulVerigetir(kullaniciId) {
        $.ajax({
            url: '/Home/OkulVeriGetir?id=' + kullaniciId,
            type: 'GET',
            data: { KullaniciId: kullaniciId },
            dataType: 'json',
            success: function (data) {
                console.log(data);
                const okulListSahte = data[0].okulBilgiVM;
                if (data[0]?.okulBilgiVM && data[0].okulBilgiVM.length > 0) {
                    data[0].okulBilgiVM.forEach(item => {
                        if (item.okulBilgisi && item.okulBilgisi.id) {
                            YeniOkulTutucu.push(item.okulBilgisi.id);
                        }
                    });
                } else {
                    YeniOkulTutucu = [];
                }
                console.log("Tüm okul idleri:", YeniOkulTutucu);
                console.log(YeniOkulTutucu);
                let FakeID = YeniOkulTutucu.length > 0 ? YeniOkulTutucu[YeniOkulTutucu.length - 1] : 0;
                console.log("FakeID: " + FakeID)
                const okulBilgiListesi = data[0].okulBilgiVM;


                //SILME BUTONU
                document.getElementById('medyaAlanlari').addEventListener('click', function (e) {
                    if (e.target.classList.contains('sil-btn')) {
                        e.preventDefault();
                        const div = e.target.closest('.media-div');
                        const okulId = div.querySelector('.okul-id-hidden')?.value;
                        if (okulId) {
                            SilinenOkulDiv.push(parseInt(okulId));
                            console.log(SilinenOkulDiv);
                        }
                        div.remove();
                    }
                });
                //EKLEME BUTONU
                document.getElementById('medyaAlanlari').addEventListener('click', function (e) {
                    if (e.target.classList.contains('ekle-btn')) {
                        counter++;
                        console.log("counter :" + counter);
                        FakeID++;
                        e.preventDefault();
                        const div = document.createElement('div');
                        div.style.boxShadow = "4px 4px 12px black";
                        div.classList.add('media-div', 'mt-3', 'bg-dark', 'rounded', 'p-2');
                        div.setAttribute('data-id', `${counter}`);
                        div.innerHTML = `
                                <h5 class="text-light">Mezuniyet Bilgisi #${counter}</h5>
                                <input type="hidden" class="okul-id-hidden" value="${counter}" />

                                <label class="form-label text-light"><strong>Okul Türü:</strong></label>
                                <select class="form-control okul-select" id="Okul${counter}">
                                    <option value="1">Lise</option>
                                    <option value="2">Üniversite</option>
                                </select>

                                <label class="form-label text-light mt-2"><strong>Mezuniyet Tarihi:</strong></label>
                                <input type="date" id="MezuniyetYili${counter}" class="form-control mezuniyet-tarihi">

                                <label class="form-label text-light mt-2"><strong>Okul Adı:</strong></label>
                                <input type="text" class="form-control okul-adi" id="OkulAdi${counter}" placeholder="Okul Adı giriniz...">

                                <label class="form-label text-light mt-2"><strong>Diploma:</strong></label>
                                <input type="file" class="form-control media-input" id="mediaIn${counter}" name="Diploma" multiple>

                                <div id="diplomaPreview${counter}" class="d-flex flex-wrap mt-2"></div>
                                <button type="button" class="btn btn-danger mt-2 sil-btn">-</button>
                                <button type="button" class="btn btn-success mt-2 ekle-btn">+</button>
                            `;
                        MedyaAlanlari.append(div);
                        YeniOkulTutucu.push(parseInt(FakeID));
                        EklenenOkulDiv.push(parseInt(FakeID));
                        console.log(YeniOkulTutucu);
                        console.log(EklenenOkulDiv);
                    }
                });
                if (!data || data.length === 0) {
                    console.warn("Okul verisi boş veya gelmedi.");
                    return;
                }



                if (!okulBilgiListesi) {
                    console.warn("OkulBilgiVM undefined.");
                    return;
                }


                MedyaAlanlari.on('change', '.media-input', function (e) {
                    const inputId = $(this).attr("id");
                    const index = inputId.replace("mediaIn", "");
                    const previewContainer = $(`#diplomaPreview${index}`);

                    Array.from(this.files).forEach(file => {
                        const reader = new FileReader();
                        reader.onload = function (event) {
                            const img = $(`                                
                                <div class="position-relative media-prev-div m-2">
                                <input type="hidden" class="okul-id-hiddenMedia" value ="0" />
                                <img src="${event.target.result}" alt="eklenen" 7
                                style="width:200px; height:200px; border:12px solid black; border-radius:43px;" />
                                <button class="btn btn-sm btn-danger position-absolute top-0 end-0 medya-sil-btn" 
                                data-media-id="0">&times;</button>
                                </div>`);
                            previewContainer.append(img);
                        };
                        reader.readAsDataURL(file);
                    });
                });


                okulBilgiListesi.forEach(function (item, index) {
                    console.log("index :" + index);
                    const okulBilgisi = item.okulBilgisi;
                    const medyaListesi = item.media;

                    const div = $(`
                    <div class="media-div mt-3 bg-dark rounded p-2" data-id="${index + 1}">
                        <h5 class="text-light">Mezuniyet Bilgisi #${index + 1}</h5>
                        <input type="hidden" class="okul-id-hidden" value="${okulBilgisi.id}" />
                        
                        <label class="form-label text-light"><strong>Okul Türü:</strong></label>
                        <select class="form-control okul-select" id="Okul${index + 1}">
                            <option value="1" ${okulBilgisi.okulTuru == 1 ? 'selected' : ''}>Lise</option>
                            <option value="2" ${okulBilgisi.okulTuru == 2 ? 'selected' : ''}>Üniversite</option>
                        </select>

                        <label class="form-label text-light mt-2"><strong>Mezuniyet Tarihi:</strong></label>
                        <input type="date" id="MezuniyetYili${index + 1}" class="form-control mezuniyet-tarihi" value="${okulBilgisi.mezuniyetYili?.substring(0, 10) || ''}">

                        <label class="form-label text-light mt-2"><strong>Okul Adı:</strong></label>
                        <input type="text" class="form-control okul-adi" id="OkulAdi${index + 1}" value="${okulBilgisi.okulAdi}" placeholder="Okul Adı giriniz...">

                        <label class="form-label text-light mt-2"><strong>Diploma:</strong></label>
                        <input type="file" class="form-control media-input" id="mediaIn${index + 1}" value=${okulBilgisi.id} multiple>
                        
                        <div id="diplomaPreview${index + 1}" class="d-flex flex-wrap mt-2"></div>
                                        <button type="button" class="btn btn-danger mt-2 sil-btn">-</button>
                                        <button type="button" class="btn btn-success mt-2 ekle-btn">+</button>
                    </div>

                `);
                    MedyaAlanlari.append(div);
                    counter = okulBilgiListesi.length;
                    const diplomaPreview = $(`#diplomaPreview${index + 1}`);
                    diplomaPreview.empty();
                    medyaListesi.forEach(function (media) {
                        const imgDiv = $(`
                                <div class="position-relative media-prev-div m-2">
                                <input type="hidden" class="okul-id-hiddenMedia" value="${media.id}" />
                                <img src="/MediaKutuphane/${media.mediaUrl}" alt="${media.mediaAdı}" 7
                                style="width:200px; height:200px; border:12px solid black; border-radius:43px;" />
                                <button class="btn btn-sm btn-danger position-absolute top-0 end-0 medya-sil-btn" 
                                data-media-id="${media.id}">&times;</button>
                                </div>
                            `);
                        diplomaPreview.append(imgDiv);
                    });
                    document.getElementById(`diplomaPreview${index + 1}`).addEventListener('click', function (e) {
                        if (e.target.classList.contains('medya-sil-btn')) {
                            e.preventDefault();
                            const div = e.target.closest('.media-prev-div');
                            const mediaId = div.querySelector('.okul-id-hiddenMedia')?.value;
                            if (mediaId == 0) {
                                div.remove();
                            }
                            if (mediaId) {
                                SilinenMedya.push(parseInt(mediaId));
                                console.log(SilinenMedya);
                            }
                            div.remove();
                        }
                    });


                    document.getElementById("GuncelleBtn").addEventListener("click", Guncelle);


                });
            }
        });
        function Guncelle() {
            var cins;
            var erkek = document.getElementById('erkekcheck').checked;
            var kadin = document.getElementById('kadincheck').checked;
            if (erkek) {
                cins = "1";
            }
            else if (kadin) {
                cins = "0";
            }
            else {
                console.log("hata cinsiyet null:" + cins);
            }
            console.log("Cinsiyet:", cins);

            var ad = $("#Ad").val();
            var soyad = $("#Soyad").val();
            var date = $("#DogumTarihi").val();
            if (!date) {
                alert("Doğum tarihi boş olamaz!");
                return;
            }
            console.log("Date:", date);
            var ulke = $("#Ulke").val();
            var sehir = $("#Sehir").val();
            var aciklama = $("#Aciklama").val();

            var model = {
                Id: kullaniciId,
                Ad: ad,
                Soyad: soyad,
                DogumTarihi: date,
                Cinsiyet: cins,
                Ulke: ulke,
                Sehir: sehir,
                Aciklama: aciklama
            };
            console.log("ajax başlıyor");
            $.ajax({
                url: "/Home/KullaniciGuncelle",
                type: "PUT",
                contentType: "application/json",
                data: JSON.stringify(model),
                success: function (data) {
                    console.log("Başarılı cevap geldi:");
                    console.log(data);
                    console.log(YeniOkulTutucu);
                    const okulIdInputs = document.querySelectorAll('.okul-id-hidden');
                    YeniOkulTutucu = Array.from(okulIdInputs).map(input => parseInt(input.value));
                    for (let i = 0; i < YeniOkulTutucu.length; i++) {

                        const okulId = YeniOkulTutucu[i];
                        const div = document.querySelector(`.media-div .okul-id-hidden[value="${okulId}"]`)?.closest('.media-div');
                        if (!div) return;

                        const okulturuEl = div.querySelector(".okul-select");
                        const mezuniyetTarihiEl = div.querySelector(".mezuniyet-tarihi");
                        const okulAdiEL = div.querySelector(".okul-adi");
                        const diplomaEL = div.querySelector(".media-input");

                        if (!okulturuEl || !mezuniyetTarihiEl || !okulAdiEL || !diplomaEL) {
                            console.warn(`Mezuniyet bilgisi #${i} için gerekli alanlar eksik.`);
                        }
                        console.log("DiplomaEl files" + diplomaEL);

                        if (!diplomaEL) {
                            console.warn(`mediaIn${i + 1} input bulunamadı.`);
                        }

                        console.log(diplomaEL)

                        const okulTuru = okulturuEl.value;
                        const mezuniyetTarihi = mezuniyetTarihiEl.value;
                        const okulAdi = okulAdiEL.value;

                        const KullaniciOkuljson = {
                            Id: okulId,
                            KullaniciId: kullaniciId,
                            OkulTuru: parseInt(okulTuru),
                            MezuniyetYili: mezuniyetTarihi,
                            OkulAdi: okulAdi
                        };

                        console.log("KullaniciOkuljson:", KullaniciOkuljson);
                        $.ajax({
                            url: '/Home/KullaniciOkulGuncelle',
                            type: 'PUT',
                            dataType: 'json',
                            contentType: 'application/json',
                            data: JSON.stringify(KullaniciOkuljson),
                            success: function (response) {
                                console.log("Kullanıcı okul bilgisi güncellendi:", response);
                                var OkulId = okulId;
                                const formMediaData = new FormData();
                                const diplomaFiles = diplomaEL.files;
                                if (!diplomaFiles || diplomaFiles.length === 0) {
                                    console.warn("Diploama dosyası boş " + diplomaFiles);
                                }
                                console.log("Input:", diplomaEL);
                                console.log("Dosyalar:", diplomaFiles);

                                if (!OkulId) {
                                    console.warn("Okul ID alınamadı, güncelleme yapılamadı.");
                                    return;
                                }
                                for (let i = 0; i < diplomaFiles.length; i++) {
                                    formMediaData.append('Diploma', diplomaFiles[i]);
                                }
                                formMediaData.append('OkulId', OkulId);
                                formMediaData.append('kullaniciId', kullaniciId);
                                for (let pair of formMediaData.entries()) {
                                    if (pair[1] instanceof File) {
                                        console.log(pair[0], pair[1].name); // Dosya ismini yazdır
                                    } else {
                                        console.log(pair[0], pair[1]); // Diğer parametreleri yazdır
                                    }
                                }
                                if (diplomaFiles) {
                                    $.ajax({
                                        url: '/Home/DiplomaKayit',
                                        type: 'POST',
                                        data: formMediaData,
                                        contentType: false,
                                        processData: false,
                                        success: function (response) {
                                            console.log("Diploma güncelleme başarılı:", response);
                                        },
                                        error: function (xhr, status, error) {
                                            console.error("Diploma güncelleme sırasında hata:", error);
                                        }
                                    });
                                }
                            },
                            error: function (xhr, status, error) {
                                console.error("Kullanıcı okul bilgisi güncellenirken hata:", error);
                            }
                        });
                    }
                    for (let i = 0; i < SilinenMedya.length; i++) {
                        console.log("Silinen medya id:", SilinenMedya[i]);
                        $.ajax({
                            url: `/Home/SilinenOkulGuncelle/${SilinenMedya[i]}`,
                            type: 'DELETE',
                            success: function (response) {

                                console.log("Silinen okul bilgileri güncellendi:", response);
                            },
                            error: function (xhr, status, error) {
                                console.error("Silinen okul bilgileri güncellenirken hata:", error);
                            }
                        });
                    }
                    for (let i = 0; i < SilinenOkulDiv.length; i++) {
                        $.ajax({
                            url: `/Home/SilinenOkulGuncelleDiv/${SilinenOkulDiv[i]}`,
                            type: 'DELETE',
                            success: function (response) {

                                console.log("Silinen okul bilgileri güncellendi:", response);
                            },
                            error: function (xhr, status, error) {
                                console.error("Silinen okul bilgileri güncellenirken hata:", error);
                            }
                        });
                    }
                    const mediaInput = document.getElementById(`mediaInput`);
                    if (mediaInput && mediaInput.files.length > 0) {
                        const YuklenenMedyaKayit = Array.from(mediaInput.files);
                        var FormMediaData = new FormData();
                        YuklenenMedyaKayit.forEach((file) => {
                            FormMediaData.append(`mediaFiles`, file);
                        });
                        FormMediaData.append('Id', data.id);

                        $.ajax({
                            url: '/Home/MediaGuncelle',
                            data: FormMediaData,
                            type: 'PUT',
                            contentType: false,
                            processData: false,
                            success: function (response) {
                                console.log("Medya kaydı başarılı:", response);

                            },
                            error: function (xhr, status, error) {
                                console.error("Medya kaydı sırasında hata:", error);
                            }
                        });
                    }
                    Swal.fire({
                        icon: 'success',
                        title: 'Güncelleme Başarılı!',
                        text: 'Bilgiler başarıyla kaydedildi.',
                        confirmButtonText: 'Tamam'
                    });
                    setTimeout(() => window.location.href = "/Home/iletisimList", 2000);

                },
                error: function (xhr) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Hata!',
                        text: 'Kayıt sırasında bir sorun oluştu.'
                    });
                }
            });
            console.log(model);
        }

    }
});

