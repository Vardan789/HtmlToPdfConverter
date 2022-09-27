function uploadFiles(inputId) {
    const input = document.getElementById(inputId);
    const files = input.files;
    const formData = new FormData();

    for (let i = 0; i !== files.length; i++) {
        formData.append("files", files[i]);
    }

    startUpdatingProgressIndicator();
    $.ajax(
        {
            url: "/uploader",
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                stopUpdatingProgressIndicator();
                const element = document.getElementById('progress');
                element.innerHTML += `<a id="downloadHref" href="${data[0]}" download="file"></a>`
                const downloadElement = document.getElementById('downloadHref');
                downloadElement.click();
                downloadElement.remove();
                alert("Files Uploaded!");
            },
            error: function (error) {
                stopUpdatingProgressIndicator();
            }
        }
    );
}

let intervalId;

function startUpdatingProgressIndicator() {
    $("#progress").show();

    intervalId = setInterval(
        function () {
            // We use the POST requests here to avoid caching problems (we could use the GET requests and disable the cache instead)
            $.post(
                "/uploader/progress",
                function (progress) {
                    $("#bar").css({width: progress + "%"});
                    $("#label").html(progress + "%");
                }
            );
        },
        10
    );
}

function stopUpdatingProgressIndicator() {
    clearInterval(intervalId);
}