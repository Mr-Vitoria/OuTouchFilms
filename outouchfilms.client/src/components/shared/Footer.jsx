import { Component } from "react";

export class Footer extends Component {

    constructor(props) {
        super(props);
    }

    render() {
        return <footer>
        <div className="block">
            <div className="slogan">
                <img src="img/miku.png" />
                <p>Смотрите аниме без рекламы</p>
            </div>
            <nav>
                <div className="group">
                    <p>О нас</p>
                    <a className="link" href="/">Лучший сайт</a>
                    <a className="link" href="https://t.me/outouch_tg">Вакансии</a>
                    <a className="link" href="/NewsDetail?newsId=35">Женский взгляд</a>
                </div>
                <div className="group">
                    <p>Разделы</p>
                    <a className="link" href="/">Главная</a>
                    <a className="link" href="/login">Личный кабинет</a>
                </div>
            </nav>
        </div>
        <div className="block">
            <p className="copyright">© 2024 | OuTouch</p>
        </div>
    </footer>
    }
}