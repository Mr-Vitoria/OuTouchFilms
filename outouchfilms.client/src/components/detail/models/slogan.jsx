import { Component } from "react";

import style from "../../../assets/css/detail.module.scss";
import { updateUserAnime } from "../../../api/animeService";

export class Slogan extends Component {
    constructor(props) {
        super(props);

        this.state = {
            isPlan: this.props.anime.isUserPlan
        }

        this.changeUserAnimeEvent = this.changeUserAnimeEvent.bind(this);
    }

    async changeUserAnimeEvent() {
        const result = await updateUserAnime(this.props.userId, this.props.anime.id, this.state.isPlan ? "None" : "PlanToWatch");

        if(result){
            this.setState({
                isPlan: !this.state.isPlan
            });
        }
    }
    render() {
        return <section className={`${style.filmSlogan}`}>
            <div className={`${style.background}`}>
                <img src={this.props.anime.screenshots[0]} />
                {
                    this.props.anime.videos[0] != undefined ?
                        <video
                            src={this.props.anime.videos[0]}
                            autoPlay={true}
                            muted={true}
                            onPlay={(ev) => {
                                setTimeout(() => {
                                    ev.target.classList.add(style.append);
                                }, 3500)
                            }}
                            onEnded={(ev) => {
                                ev.target.classList.remove('append');

                                setTimeout(() => {
                                    ev.target.play();
                                }, 1500)
                            }}
                        >
                        </video>
                        : null
                }
            </div>

            <div className={`${style.textContainer}`}>
                <div className={`${style.content}`}>
                    <a href="/" className={style.arrowBack}>
                        <img src="img/ico/arrowBack.svg" />
                    </a>

                    <div className={style.shortInfo}>
                        <h2>{this.props.anime.name}</h2>
                        <p>{this.props.anime.year} | {this.props.anime.duration} мин. | {this.props.anime.type != "Фильм" ? `${this.props.anime.currentEpisodes} серий` : ""}</p>
                    </div>

                    <div className={style.btnContainer}>
                        <button
                            className="btn pay"
                            onClick={(ev) => {
                                window.location.assign("#player");
                            }}
                        >
                            Смотреть
                        </button>
                        <button
                            className="btn btnDark"
                            onClick={(ev) => {
                                window.location.assign("#trailerList");
                            }}
                        >
                            <img src="img/ico/play.svg" /> Трейлер
                        </button>
                    </div>

                    {
                        this.props.userId != undefined ?
                            <div className={`${style.btnContainer} ${style.small}`}>
                                <button
                                    className="btn btnDark"
                                    onClick={(ev) => {
                                        this.changeUserAnimeEvent();
                                    }}
                                >
                                    <img src={`${this.state.isPlan ? "img/ico/heart-fill.png":"img/ico/heart.png"}`} /> Добавить в запланированные
                                </button>
                            </div>
                            : null
                    }

                </div>
            </div>
        </section>
    }
}