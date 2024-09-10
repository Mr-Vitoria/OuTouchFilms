import React, { Component } from "react";

import style from "../../../assets/css/profile.module.scss";

import { updateUser } from "../../../api/userService";

export class ModalUserInfo extends Component {

    constructor(props) {
        super(props);

        this.sectionRef = React.createRef();

        this.updateUserEvent = this.updateUserEvent.bind(this);

        this.open = this.open.bind(this);
    }

    open() {
        this.sectionRef.current.classList.add("active");
    }

    async updateUserEvent(formData) {
        const result = await updateUser(formData);

        if (result == true) {
            alert("OK");
        }
        else {
            alert("NOT OK");
        }
    }

    render() {
        return <section ref={this.sectionRef} className={`modalContainer userInfoModal ${style.userInfoModal}`}>
            <div className={`modal ${style.modal}`}>
                <div className={`header ${style.header}`}>
                    <button onClick={(ev) => {
                        this.sectionRef.current.classList.remove("active");
                    }}>
                        <img src="img/ico/x_mark.svg" />
                    </button>
                    <p>Настройки</p>
                </div>

                <form
                    className={`body ${style.body}`}
                    onSubmit={(ev) => {
                        this.updateUserEvent(ev.target);
                    }}
                >
                    <div className={`${style.inputContainer}`}>
                        <label>Имя</label>
                        <input
                            className={`input`}
                            placeholder="Имя"
                            type="text"
                            defaultValue={this.props.user.login}
                            required={true}
                            autoComplete="username"
                        />
                    </div>

                    <div className={`${style.inputContainer}`}>
                        <label>Email</label>
                        <input
                            className={`input`}
                            placeholder="Email"
                            type="email"
                            defaultValue={this.props.user.email}
                            required={true}
                            autoComplete="email"
                        />
                    </div>

                    <div className={`${style.inputContainer}`}>
                        <label>Пароль</label>
                        <input
                            className={`input`}
                            placeholder="Пароль"
                            type="password"
                            defaultValue={this.props.user.password}
                            autoComplete="new-password"
                            required={true}
                        />
                    </div>
                    <div className={`${style.footer}`}>
                        <button type="submit" className="btn">Изменить</button>
                    </div>
                </form>
            </div>
        </section>
    }
}