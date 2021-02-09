{
    document.querySelectorAll('.dropzone').forEach(dropzone => setDropzone(dropzone));

    function setDropzone(dropzone) {
        const input = dropzone.querySelector('.dropzone__input');
        const prompt = dropzone.querySelector('.dropzone__prompt').dataset.prompt;
        dropzone.querySelector('.dropzone__prompt').innerHTML = prompt;

        input.addEventListener('change', e => {
            if (e.target.files.length) {
                setImage(dropzone, e.target.files[0]);
            }
        });

        dropzone.querySelector('.dropzone__remove').addEventListener('click', e => {
            resetImage(dropzone, prompt);
            input.value = '';
            e.stopPropagation();
        })

        dropzone.addEventListener('click', () => input.click());

        dropzone.addEventListener('drop', e => {
            e.preventDefault();

            if (e.dataTransfer.files.length) {
                input.files = e.dataTransfer.files;
                setImage(dropzone, input.files[0]);
            }

            dropzone.classList.remove('dropzone--over');
        });

        dropzone.addEventListener('dragover', e => {
            e.preventDefault();
            dropzone.classList.add('dropzone--over');
        });

        ['dragleave', 'dragend'].forEach(e => {
            dropzone.addEventListener(e, () => {
                dropzone.classList.remove('dropzone--over');
            });
        });
    }

    function setImage(dropzone, image) {
        if (!isImage(image)) {
            return;
        }

        let promptElement = dropzone.querySelector('.dropzone__prompt');
        if (promptElement != null) {
            promptElement.remove();
        }

        let thumbnailElement = dropzone.querySelector('.dropzone__thumb');
        if (!thumbnailElement) {
            thumbnailElement = document.createElement("div");
            thumbnailElement.classList.add('dropzone__thumb');
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

        dropzone.classList.add('dropzone--filled');
    }

    function resetImage(dropzone, prompt) {
        let thumbnailElement = dropzone.querySelector('.dropzone__thumb');
        if (thumbnailElement) {
            thumbnailElement.remove();
        }

        let promptElement = dropzone.querySelector('.dropzone__prompt');
        if (!promptElement) {
            promptElement = document.createElement('span');
            promptElement.classList.add('dropzone__prompt');
            promptElement.innerHTML = prompt;
            dropzone.appendChild(promptElement);
        }

        dropzone.classList.remove('dropzone--filled');
    }

    function isImage(file) {
        return file.type.startsWith('image/');
    }
}