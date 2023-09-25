let controller;
let signal;
let endSearch = true;


let searchForms = document.getElementsByClassName("searchForm");

for (var i = 0; i < searchForms.length; i++) {
    let titleFilm = searchForms[i].getElementsByClassName("title")[0];
    let helpSearch = searchForms[i].getElementsByClassName("helpSearch")[0];
    let allFilmsHref = searchForms[i].getElementsByClassName("allFilms")[0];

    titleFilm.addEventListener("input", (event) => {
        let title = event.target.value;
        if (title.length >= 3) {
            if (!endSearch) {
                abortSearch();
            }
            beginSearch(event.target.value, helpSearch);
            allFilmsHref.href = window.location.origin + '/Films/Search/?title=' + title;
            allFilmsHref.classList.remove("d-none");
        }
        else {
            helpSearch.getElementsByClassName("filmList")[0].innerHTML = "";
            allFilmsHref.classList.add("d-none");
        }
    });

    titleFilm.addEventListener("focus", (event) => {
        helpSearch.classList.remove("d-none");
    });


    titleFilm.addEventListener("blur", (event) => {
        document.addEventListener("click", onBlurInput);
        function onBlurInput(event) {
            console.log(event);
            if (event.target.offsetParent == null || !event.target.offsetParent.classList.contains("helpSearch")) {
                helpSearch.classList.add("d-none");
            }
            document.removeEventListener("click", onBlurInput);

        }

    });
}



function beginSearch(title, helpSearch) {
    endSearch = false;
    controller = new AbortController();
    signal = controller.signal
    var urlSearch = window.location.origin + '/Films/GetFilmsByTitle/?title=' + title +
        '&count=' + 5;


    let spinner = helpSearch.getElementsByClassName("spinnerSearch")[0];
    let filmList = helpSearch.getElementsByClassName("filmList")[0];

    spinner.classList.remove("d-none");
    filmList.classList.add("d-none");

    fetch(urlSearch, {
        method: 'get',
        signal: signal,
    })
        .then((response) => {
            return response.json();
        })
        .then((result) => {
            drawSearchFilms(result, filmList, spinner);
            endSearch = true;
        }).catch(function (err) {
            console.error(` Err: ${err}`);
            endSearch = true;
        });
}


function abortSearch() {
    console.log('Now aborting');
    // Abort.
    controller.abort();
    endSearch = true;
}


function drawSearchFilms(films, filmList, spinner) {
    filmList.innerHTML = "";
    if (films.length > 0) {

        for (var i = 0; i < films.length; i++) {

            filmList.appendChild(getFilmSearch(films[i]));
        }
    }
    else {
        let p = document.createElement("p");
        p.textContent = "Совпадений не найдено";
        p.classList.add("nofilm");

        filmList.appendChild(p);
    }
    filmList.classList.remove("d-none");
    spinner.classList.add("d-none");

}

function getFilmSearch(film) {
    let a = document.createElement("a");
    a.href = window.location.origin + "/Films/Details/" + film.id;
    console.log(film);
    let filmSearchDiv = document.createElement("div");
    filmSearchDiv.classList.add("filmSearch");

    let filmImg = document.createElement("img");
    filmImg.src = film.poster;

    let filmP = document.createElement("p");
    filmP.textContent = film.title;

    filmSearchDiv.appendChild(filmImg);
    filmSearchDiv.appendChild(filmP);

    a.appendChild(filmSearchDiv);

    return a;
}