
let kodikUrl = '';
let iframeFilm = document.getElementById("FilmPlayer");

let seriaDivs = document.getElementsByClassName("seriesNumber");
let activeSeriaDiv = document.getElementsByClassName("seriesNumber active")[0];
let activeSeria = 0;
let translationSelect = document.querySelector(".translationsContanier select");
let activeTranslation = 0;
let lastTranslation = 0;
let inputSeria = document.getElementById("inputSeria");

let carouselSeria = $("#carouselSeries");
let countSeriesinCarousel = document.getElementById("carouselSeries").style.width;

let playerDuration = 4000;

let autoChange = false;

let achievements;

inputSeria.addEventListener("change", (event) => {
    let seriaDiv = undefined;
    for (let elem of seriaDivs) {
        if (elem.textContent == event.target.value) {
            seriaDiv = elem;
        }
    }

    changeSeries(event.target.value, seriaDiv)
})

function changeSeries(seria, seriaDiv) {
    activeSeriaDiv.classList.remove("active");
    seriaDiv.classList.add("active");
    activeSeriaDiv = seriaDiv;
    activeSeria = seria;
    document.cookie = "lastSeria=" + seria + "; path=" + document.location.pathname;
    inputSeria.value = seria;

    let countSeriesinCarousel = Math.floor(carouselSeria.width() / $(seriaDiv).width())-2;
    let toSeria = activeSeria - 1;
    if (toSeria + countSeriesinCarousel > lastEpisode) {
        toSeria = lastEpisode - countSeriesinCarousel;
        
    }
    carouselSeria.trigger('to.owl.carousel', [toSeria, 400]);


    changePlayer();
}
function changeTranslation(translationId) {
    activeTranslation = translationId;
    document.cookie = "lastTranslation=" + activeTranslation + "; path=" + document.location.pathname;
    changePlayer();
}
function changePlayer() {
    iframeFilm.src = kodikUrl + '?episode=' + activeSeria + '&only_episode=true'
        + '&only_translations=' + activeTranslation
        + '&translations=false&auto_translation=true';

}

let lastSeria = getCookie("lastSeria");
if (lastSeria == undefined) {
    lastSeria = "1";
}




translationSelect.addEventListener("change", (event) => {
    changeTranslation(event.target.value)
})









let lastEpisode = 0;
let userId = getCookie("id");
let filmId = 0;
let typeFilm = '';



function kodikMessageListener(message) {

    if (message.data.key == 'kodik_player_play') {

        if (userId != undefined) {
            fetch(window.location.origin + '/Film/ChangeUsersFilmFromJs/?userId=' + userId +
                '&filmId=' + filmId +
                '&typeFilmUser=' + 'Watching')
                .then((response) => {
                    return response.json();
                });
        }
    }


    if (message.data.key == 'kodik_player_current_episode') {
        if (activeSeria == lastEpisode) {

            if (userId != undefined) {
                fetch(window.location.origin + '/Film/ChangeUsersFilmFromJs/?userId=' + userId +
                    '&filmId=' + filmId +
                    '&typeFilmUser=' + 'Completed')
                    .then((response) => {
                        return response.json();
                    });
            }
        }

    }
    if (message.data.key == 'kodik_player_current_episode') {
        if (activeSeria == lastEpisode) {

            if (userId != undefined) {
                fetch(window.location.origin + '/Film/ChangeUsersFilmFromJs/?userId=' + userId +
                    '&filmId=' + filmId +
                    '&typeFilmUser=' + 'Completed')
                    .then((response) => {
                        return response.json();
                    });
            }
        }

    }
    if (message.data.key == 'kodik_player_duration_update') {
        playerDuration = Math.floor(message.data.value);

    }
    if (message.data.key == 'kodik_player_time_update') {
        if (userId != undefined) {

            checkAchievments(message.data.value);
        }
        if (autoChange && playerDuration == message.data.value) {
            nextSeria();
        }

    }
}
let activeAchievement;

function checkAchievments(playerTime) {
    for (var i = 0; i < achievements.length; i++) {
        if (activeSeria != achievements[i].SeriaNumber) {
            continue;
        }

        if (playerTime == achievements[i].SecondsFromStart) {
            activeAchievement = achievements[i];
            fetch(window.location.origin + '/User/AddUserAсhievment/?userId=' + userId +
                '&achievmentId=' + achievements[i].Id)
                .then((response) => {
                    return response.text();
                })
                .then((result) => {
                    if (result == "true") {
                        drawAchievement(activeAchievement);
                    }
                });
        }
    }
}
const removeAchievementAnimation = [
    { opacity: "1" },
    { opacity: "0" },
];

const removeAchievementTiming = {
    duration: 1400,
    iterations: 1,
};

function drawAchievement(achievement) {
    let achievementContainer = document.createElement("div");
    achievementContainer.classList.add("achievementContainer");
        let achievementBlock = document.createElement("div");
        achievementBlock.classList.add("achievementBlock");

            let achievementImg = document.createElement("img");
            achievementImg.src = achievement.ImgUrl;

            let description = document.createElement("div");
            description.classList.add("description");

                let nameP = document.createElement("p");
                nameP.textContent = achievement.Name;
                let spanX = document.createElement("span");
                spanX.style = "float:right; font-size:2em; margin-top:-15px;";
                    spanX.textContent = "x";
                    spanX.addEventListener("click", () => {
                    
                        achievementBlock.animate(removeAchievementAnimation, removeAchievementTiming).addEventListener("finish", () => {
                            document.body.removeChild(achievementContainer);
                        });
                    })
                nameP.appendChild(spanX);

                let descriptionP = document.createElement("p");
                descriptionP.textContent = achievement.Description;
            description.appendChild(nameP);
            description.appendChild(descriptionP);

        achievementBlock.appendChild(achievementImg);
        achievementBlock.appendChild(description);


    achievementContainer.appendChild(achievementBlock);
    document.body.appendChild(achievementContainer);
}


function nextSeria() {
    if (activeSeria != lastEpisode) {
        changeSeries(Number(activeSeria) + 1, seriaDivs[activeSeria]);
    }
}

if (window.addEventListener) {
    window.addEventListener('message', kodikMessageListener);
} else {
    window.attachEvent('onmessage', kodikMessageListener);
}



function initializate(_kodikUrl, _lastTranslation, _lastEpisode, _filmId, _typeFilm, _achievements) {
    kodikUrl = _kodikUrl;
    lastTranslation = _lastTranslation;
    lastEpisode = _lastEpisode;
    filmId = _filmId;
    typeFilm = _typeFilm;
    achievements = _achievements;


    let lastSeriaDiv = document.getElementsByClassName("seriesNumber")[lastSeria-1];
    changeSeries(lastSeria, lastSeriaDiv);
    changeTranslation(lastTranslation);


}


function changeAutoChange() {
    autoChange = !autoChange;
}