import React, { Component } from "react";

import style from "../../../assets/css/login.module.scss";
import { registration } from "../../../api/userService";
import { setCookie } from "../../../functions/cookie";

export class RegistrationForm extends Component {

    constructor(props) {
        super(props);

        this.containerRef = React.createRef();
        this.registrationEvent = this.registrationEvent.bind(this);
    }

    async registrationEvent(formData) {
        const result = await registration(formData);


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
                <p className={`${style.title}`}>Регистрация</p>
                <p className={`${style.description}`}>Зарегестрируйтесь, чтобы получить доступ к нашей базе</p>
                <button className="btn" onClick={(ev) => {
                    this.props.clearContainer();
                    this.containerRef.current.classList.add(style.active);
                }}>Зарегестрироваться</button>
            </div>

            <form onSubmit={(ev) => {
                ev.preventDefault();
                this.registrationEvent(ev.target);
            }}>
                <input type="text" name="login" placeholder="Логин..." autoComplete="username" required />
                <input type="email" name="email" placeholder="Email..." autoComplete="email" required />
                <input type="password" name="password" placeholder="Пароль..." autoComplete="new-password" required />

                <button className="btn" type="submit">Зарегестрироваться</button>
            </form>
        </div>
    }
}