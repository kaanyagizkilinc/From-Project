document.addEventListener('DOMContentLoaded', function () {
    function getQueryParam(name) {
        const urlParams = new URLSearchParams(window.location.search);
        return urlParams.get(name);
    }

    const id = getQueryParam("id");

    $.ajax({
        url: '/Home/MedyaGetir?id=' + id,
        type: 'GET',
        dataType: 'json',
        success: function (medyaListesi) {
            let prewDiv = document.getElementById('preview');
            if (!prewDiv) {
                prewDiv = document.createElement('div');
                prewDiv.id = 'preview';
                document.body.appendChild(prewDiv);
            }

            prewDiv.innerHTML = '';

            if (medyaListesi && medyaListesi.length > 0) {
                medyaListesi.forEach(function (medya) {
                    if (medya.media && medya.media.mediaUrl) {
                        const imgSrc = '/MediaKutuphane/' + medya.media.mediaUrl;
                        const imgEl = document.createElement('img');
                        imgEl.src = imgSrc;
                        imgEl.alt = medya.media.mediaAdı || '';
                        imgEl.style.width = '200px';
                        imgEl.style.height = '200px';
                        imgEl.style.marginTop = '10px';
                        imgEl.style.border = '12px solid black';
                        imgEl.style.borderRadius = '43px';
                        imgEl.style.display = 'block';  
                        prewDiv.appendChild(imgEl);
                    }
                });
            }
        },
        error: function (xhr, status, error) {
            console.error("Medya verisi alınamadı:", error);
        }
    });

    const mediaInput = document.getElementById('mediaInput');
    const prew = document.getElementById('prew'); 
    const prewDiv = document.getElementById('preview'); 

    mediaInput.addEventListener('change', function () {
        if (mediaInput.files.length > 0) {
            if (prewDiv) {
                prewDiv.style.display = 'none';
            }

            const file = mediaInput.files[0];
            prew.src = URL.createObjectURL(file);
            prew.style.display = 'block';
        } else {

            if (prewDiv) {
                prewDiv.style.display = 'block';
            }
            prew.style.display = 'none';
            prew.src = '';
        }
    });
});
