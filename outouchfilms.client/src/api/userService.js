import { API_URL } from "./variables/variables";


export async function login(email, password) {
    try {
        const response = await fetch(API_URL + "api/auth/login?email=" + email + "&password=" + password, {
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

export async function registration(formData) {
    try {
        const response = await fetch(API_URL + "api/auth/registration", {
            method: "POST",
            body: new FormData(formData)
        });

        if (response.status == 200) {
            return await response.json();
        }

    } catch (error) {
        console.log(error);
        return false;
    }
}

export async function getUser(token) {
    try {
        const response = await fetch(API_URL + "api/user/getByToken?token=" + token, {
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

export async function updateUser(formData, token) {
    try {
        const response = await fetch(API_URL + "api/user/update?token=" + token, {
            method: "POST",
            body: new FormData(formData)
        });

        if (response.status == 200) {
            return await response.json();
        }

    } catch (error) {
        console.log(error);
        return false;
    }
}