import { Component } from "react";

import style from "../../../assets/css/detail.module.scss";

export class AnimeInfo extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return <section className={`${style.infoSection}`}>
            <div className={`${style.plot}`}>
                <h3>Сюжет</h3>
                <p>{
                    this.props.anime.description != "" ?
                        this.props.anime.description
                        :
                        "Админ поленился и не написал описание к аниме. Проблемы?"
                }</p>
            </div>

            <div className={`${style.infoContainer}`}>
                <h3>Информация</h3>
                <div className={`${style.content}`}>
                    <div className={`${style.fieldList}`}>
                        {
                            this.props.anime.countries.count > 0 ?
                                <div className={`${style.field}`}>
                                    <p className={`${style.title}`}>Страна</p>
                                    <p className={`${style.values}`}>
                                        {
                                            this.props.anime.countries.map((country, key) => {
                                                return <a href="#" key={`${country.id}`}> {country.name} </a>
                                            })
                                        }
                                    </p>
                                </div>
                                : null
                        }
                        <div className={`${style.field}`}>
                            <p className={`${style.title}`}>Жанр</p>
                            <p className={`${style.values}`}>
                                {
                                    this.props.anime.genres.map((genre, key) => {
                                        return <a href="#" key={`${genre.id}`}> {genre.title} </a>
                                    })
                                }
                            </p>
                        </div>
                    </div>
                    <div className={`${style.ratingContainer}`}>
                        <p className={`${style.value}`}>{this.props.anime.score}</p>
                        <div className={`${style.count}`}>
                            <h5>Рейтинг</h5>
                            <p></p>
                        </div>
                        <button className="btn btnDark">Оценить</button>
                    </div>
                </div>
            </div>
        </section>
    }
}