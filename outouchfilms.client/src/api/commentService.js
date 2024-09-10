import { API_URL } from "./variables/variables";


export async function getCommentList(filmId) {
    try {
        const response = await fetch(API_URL + "api/comment/getList?filmId=" + filmId, {
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