document.querySelectorAll('.dropzone-item').forEach(dropzone => setDropzoneItem(dropzone));

function setDropzoneItem(dropzone) {
    const input = dropzone.querySelector('.dropzone-item__input');
    input.name = dropzone.closest('.dropzone-list').dataset.imgsName;

    input.addEventListener('change', e => {
        if (e.target.files.length) {
            setImage(dropzone, e.target.files[0], !dropzone.querySelector(".dropzone-item__thumb"));
        }
    });

    dropzone.querySelector('.dropzone-item__remove').addEventListener('click', e => {
        removeDropzone(dropzone);
        e.stopPropagation();
    })

    dropzone.addEventListener('click', () => input.click());

    dropzone.addEventListener('drop', e => {
        e.preventDefault();

        if (e.dataTransfer.files.length) {
            input.files = e.dataTransfer.files;
            setImage(dropzone, input.files[0], !dropzone.querySelector(".dropzone-item__thumb"));
        }

        dropzone.classList.remove('dropzone-item--over');
    });

    dropzone.addEventListener('dragover', e => {
        e.preventDefault();
        dropzone.classList.add('dropzone--over');
    });

    ['dragleave', 'dragend'].forEach(ev => {
        dropzone.addEventListener(ev, e => {
            dropzone.classList.remove('dropzone--over');
        });
    });
}

function setImage(dropzone, image, addNewDropzone) {
    if (!isImage(image)) {
        return;
    }

    let promptElement = dropzone.querySelector(".dropzone-item__prompt");
    if (promptElement != null) {
        promptElement.remove();
    }

    let thumbnailElement = dropzone.querySelector(".dropzone-item__thumb");
    if (!thumbnailElement) {
        thumbnailElement = document.createElement("div");
        thumbnailElement.classList.add("dropzone-item__thumb");
        dropzone.appendChild(thumbnailElement);
    }

    const reader = new FileReader();
    reader.readAsDataURL(image);
    reader.onload = () => {
        thumbnailElement.style.backgroundImage = `url('${reader.result}')`;
        if (addNewDropzone) {
            addDropzone(dropzone.closest('.dropzone-list'));
        }
    };

    dropzone.classList.add("dropzone-item--filled");
}

function isImage(file) {
    return file.type.startsWith("image/");
}

function addDropzone(dropzoneList) {
    let dropzone = document.createElement('li');
    dropzone.classList.add('dropzone-item');
    let input = document.createElement('input');
    input.classList.add('dropzone-item__input');
    input.type = 'file';
    input.accept = 'image/*';
    let remove = document.createElement('span');
    remove.classList.add('dropzone-item__remove');
    let prompt = document.createElement('span');
    prompt.classList.add('dropzone-item__prompt');

    dropzone.appendChild(input);
    dropzone.appendChild(remove);
    dropzone.appendChild(prompt);
    dropzoneList.appendChild(dropzone);

    setDropzoneItem(dropzone);
}

function removeDropzone(dropzone) {
    dropzone.closest('ul').removeChild(dropzone);
}