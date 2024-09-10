import { API_URL } from "./variables/variables";


export async function getListCurrentSeason() {
    try {
        const response = await fetch(API_URL + "api/film/getListCurrentSeason", {
            method: "GET"
        });
        if (response.status == 200) {
            return await response.json();
        }

    } catch (error) {
        console.log(error);
        return false;
    }
}

export async function getFilm(id, userId = -1) {
    try {
        const response = await fetch(API_URL + `api/film/get?id=${id}&needFull=true&userId=${userId}`, {
            method: "GET"
        });

        if (response.status == 200) {
            return await response.json();
        }

    } catch (error) {
        console.log(error);
        return false;
    }
}

export async function getUserFilmList(userId, type = 'all', count, page) {
    try {
        const response = await fetch(API_URL + `api/userfilm/getList?userId=${userId}&type=${type}&count=${count}&page=${page}`, {
            method: "GET"
        });

        if (response.status == 200) {
            return await response.json();
        }

    } catch (error) {
        console.log(error);
        return false;
    }
}

export async function geFranchiseFilmList(franchise) {
    try {
        const response = await fetch(API_URL + `api/film/getListByFranchise?franchise=${franchise}`, {
            method: "GET"
        });

        if (response.status == 200) {
            return await response.json();
        }

    } catch (error) {
        console.log(error);
        return false;
    }
}

export async function updateUserFilm(userId, filmId, type) {
    try {
        const response = await fetch(API_URL + `api/userfilm/update?userId=${userId}&filmId=${filmId}&type=${type}`, {
            method: "POST"
        });

        if (response.status == 200) {
            return await response.json();
        }

    } catch (error) {
        console.log(error);
        return false;
    }
}

export async function getRandomFilmIndex() {
    try {
        const response = await fetch(API_URL + `api/film/getrandomindex`, {
            method: "GET"
        });

        if (response.status == 200) {
            return await response.json();
        }

    } catch (error) {
        console.log(error);
        return false;
    }
}

export async function getTodayShedule() {
    try {
        const response = await fetch(API_URL + `api/film/getTodayShedule`, {
            method: "GET"
        });

        if (response.status == 200) {
            return await response.json();
        }

    } catch (error) {
        console.log(error);
        return false;
    }
}

export async function getSheduleList() {
    try {
        const response = await fetch(API_URL + `api/film/getSheduleList`, {
            method: "GET"
        });

        if (response.status == 200) {
            return await response.json();
        }

    } catch (error) {
        console.log(error);
        return false;
    }
}

let controller, signal;

export async function getFilmListByTitle(title, count = 5, page = 0) {
    try {
        if (signal != undefined && !signal.aborted) {
            controller.abort();
        }

        controller = new AbortController()
        signal = controller.signal;

        const response = await fetch(API_URL + `api/film/search?title=${title}&count=${count}&page=${page}`, {
            method: "GET",
            signal: signal
        })

        if (response.status == 200) {
            return await response.json();
        }
        else {
            return false;
        }

    } catch (error) {
        console.log(error);
        return false;
    }
}