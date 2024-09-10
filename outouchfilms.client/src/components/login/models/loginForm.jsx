import React, { Component } from "react";

import style from "../../../assets/css/login.module.scss";
import { login } from "../../../api/userService";
import { setCookie } from "../../../functions/cookie";

export class LoginForm extends Component {

    constructor(props) {
        super(props);

        this.containerRef = React.createRef();
        this.loginEvent = this.loginEvent.bind(this);
    }

    async loginEvent(formData) {
        const result = await login(formData.email.value, formData.password.value);

        if (result.status == true) {
            setCookie("token", result.message);
            setCookie("userId", result.userId);
            window.location.assign("profile");
        }
        else {
            alert(result.message);
        }
    }

    render() {
        return <div
            ref={this.containerRef}
            className={`${style.container}`}>
            <button
                className={`${style.return}`}
                onClick={(ev) => {
                    this.containerRef.current.classList.remove(style.active);
                }}
            >
                <img src="img/ico/arrowBack.svg" />
            </button>

            <div className={`${style.message}`}>
                <p className={`${style.title}`}>Вход</p>
                <p className={`${style.description}`}>Войдите, чтобы продолжить смотреть аниме</p>
                <button className="btn" onClick={(ev) => {
                    this.props.clearContainer();
                    this.containerRef.current.classList.add(style.active);
                }}>Вход</button>
            </div>

            <form onSubmit={(ev) => {
                ev.preventDefault();
                this.loginEvent(ev.target);
            }}>
                <input type="email" name="email" placeholder="Email..." autoComplete="email" required />
                <input type="password" name="password" placeholder="Пароль..." autoComplete="current-password" required />

                <button className="btn" type="submit">Войти</button>
            </form>
        </div>
    }
}