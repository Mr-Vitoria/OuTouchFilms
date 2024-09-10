import React, { Component } from "react"

import { RegistrationForm } from "./models/registrationForm";
import { LoginForm } from "./models/loginForm";
import { getCookie } from "../../functions/cookie";

import style from "../../assets/css/login.module.scss";

export class LoginContainer extends Component {

    constructor(props) {
        super(props);

        this.loginFormRef = React.createRef();
        this.registrationFormRef = React.createRef();

        this.clearContainer = this.clearContainer.bind(this);
    }

    componentDidMount() {
        if (getCookie('token') != undefined) {
            window.location.assign('profile');
        }
    }

    render() {
        return <section className={`${style.loginSection}`}>
            <div className={`${style.content}`}>
                <LoginForm ref={this.loginFormRef} clearContainer={this.clearContainer} />
                <RegistrationForm ref={this.registrationFormRef} clearContainer={this.clearContainer} />
            </div>
        </section>
    }

    clearContainer() {
        this.loginFormRef.current.containerRef.current.classList.remove(style.active);
        this.registrationFormRef.current.containerRef.current.classList.remove(style.active);
    }
}