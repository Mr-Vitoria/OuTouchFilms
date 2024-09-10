import React, { Component } from "react";

import style from "../../../assets/css/profile.module.scss";

import { deleteCookie } from "../../../functions/cookie";
import { ModalUserInfo } from "./modalUserInfo";

export class UserInfo extends Component{

    constructor(props){
        super(props);

        this.modalUserRef = React.createRef();

        this.exitEvent = this.exitEvent.bind(this);
        
        let greeting = "Доброе утро";

        let hour = new Date().getHours();

        if(hour >= 0 && hour < 4){
            greeting = "Доброй ночи";
        }
        else if (hour >= 4 && hour < 12) {
            greeting = "Доброе утро";
        }
        else if (hour >= 12 && hour < 16) {
            greeting = "Добрый день";
        }
        else if (hour >= 16 && hour < 24) {
            greeting = "Добрый вечер";
        }

        this.state = {
            greeting: greeting
        }
    }

    exitEvent(){
        deleteCookie("token");
        deleteCookie("userId");
        window.location.assign('/');
    }

    render(){
        return <section className={`${style.infoSection}`}>
            <p className={`${style.greeting}`}>{this.state.greeting}, {this.props.user.login}</p>

            <div className={`${style.actionContainer}`}>

                <div className={`${style.action}`}>
                    <div className={`${style.header}`}>
                        <p className={`${style.title}`}>Изображение профиля</p>
                    </div>
                    <p className={`${style.description}`}>Выберите или измените изображение профиля</p>
                    <img onClick={(ev) => {

                    }} src={this.props.user.imgUrl} />
                </div>
                
                <div className={`${style.action}`}>
                    <div className={`${style.header}`}>
                        <p className={`${style.title}`}>Прочие настройки</p>
                    </div>
                    <p className={`${style.description}`}>Измените логин, пароль и другую информацию о профиле</p>
                    <button 
                        onClick={(ev) => {
                            this.modalUserRef.current.open();
                        }} 
                        className={`btn ${style.btn} ${style.right}`}
                    >
                        <img src="img/ico/wheel.svg"/> Изменить
                    </button>
                </div>
                
                <div className={`${style.action}`}>
                    <div className={`${style.header}`}>
                        <p className={`${style.title}`}>Уведомления</p>
                        <label className={`${style.switch}`}>
                            <input type="checkbox" defaultChecked={this.props.user.needEmailSend} onChange={(ev) => {

                            }}/>
                            <span className={`${style.slider} ${style.round}`}></span>
                        </label>
                    </div>
                    <p className={`${style.description}`}>Отключив уведомления, вы пропустите ровным счетом ничего</p>
                </div>
            </div>

            <button onClick={(ev) => {
                this.exitEvent();
            }} className={`btn exit ${style.exit} ${style.btn}`}>Выйти из профиля</button>

            <ModalUserInfo ref={this.modalUserRef} user={this.props.user}/>
        </section>
    }
}