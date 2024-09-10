import { API_URL } from "./variables/variables";

export async function getFilmGroupList(count, page) {
    try {
        const response = await fetch(API_URL + `api/filmgroup/getList?count=${count}&page=${page}`, {
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