import React, { Component } from "react";

import style from "../../../assets/css/detail.module.scss";

export class ScreenshotModal extends Component {
    constructor(props) {
        super(props);

        this.modalContainer = React.createRef();
        this.modalImg = React.createRef();

        this.close = this.close.bind(this);
        this.open = this.open.bind(this);
    }

    close() {
        this.modalContainer.current.classList.remove('active');
    }

    open(imgUrl) {
        this.modalImg.current.src = imgUrl;
        this.modalContainer.current.classList.add('active');
    }

    render() {
        return <section ref={this.modalContainer} className={`modalContainer ${style.imageModal}`} onClick={(ev) => {
            this.close();
        }}>
            <div className={`modal ${style.modal}`}>
                <div className={`header ${style.header}`}>
                    <button onClick={(ev) => {
                        this.close();
                    }}>
                        <img src="img/ico/x_mark.svg" alt="Закрыть" />
                        <p></p>
                    </button>
                </div>

                <div className={`body ${style.body}`}>
                    <img ref={this.modalImg} src="" alt="Изображение" />
                </div>
            </div>
        </section>
    }
}