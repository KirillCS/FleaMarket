document.querySelectorAll(".drop-zone__selector__input").forEach(input => {
    const selector = input.closest(".drop-zone__selector");
    const imagesList = selector.closest(".drop-zone").getElementsByClassName("images-list")[0];

    input.addEventListener("change", e => {
        if (e.target.files.length) {
            addImagesToList(imagesList, e.target.files);
        }
    });

    selector.addEventListener("click", () => {
        input.click();
    });

    selector.addEventListener("drop", e => {
        e.preventDefault();

        if (e.dataTransfer.files.length) {
            input.files = e.dataTransfer.files;
            addImagesToList(imagesList, input.files);
        }

        selector.classList.remove("drop-zone--over");
    });

    selector.addEventListener("dragover", e => {
        e.preventDefault();
        selector.classList.add("drop-zone--over");
    });

    ["dragleave", "dragend"].forEach(type => {
        selector.addEventListener(type, e => {
            selector.classList.remove("drop-zone--over");
        });
    });
});

function addImagesToList(imagesList, images) {
    if (!imagesList || !images) {
        return;
    }

    let imgsName = imagesList.dataset.imgsName;

    for (let i = 0; i < images.length; i++) {
        const image = images[i];
        if (!isImage(image)) {
            continue;
        }

        const reader = new FileReader();
        reader.readAsDataURL(image);
        reader.onload = () => {
            imagesList.innerHTML += 
            `<li class="images-list__item">
                <img class="images-list__item__image" name="${imgsName}" src="${reader.result}" alt="">
                <span class="images-list__item__remove" onclick="removeImage(this)"></span>
            </li>`
        }
    }
}

function isImage(file) {
    return file.type.startsWith("image/");
}

function removeImage(removeButton) {
    let li = removeButton.closest("li");
    let ul = li.closest("ul");
    ul.removeChild(li);
}