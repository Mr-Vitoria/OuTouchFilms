import { Component } from "react";

import style from "../../assets/css/index.module.scss";

export class AnimeCard extends Component {
    constructor(props) {
        super(props);
        this.state = {
            style: this.props.style ?? style,
            animeId: this.props.watchAnimeId ?? -1
        }
    }

    render() {
        return <a
            href={`detail?id=${this.props.anime.id}`}
            className={`${this.state.style.card} ${this.state.animeId == this.props.anime.id ? this.state.style.watching : ""}`}
            style={{
                backgroundImage: `url("${this.props.anime.poster}")`
            }}>
            <div className={this.state.style.content}>
                <span className={this.state.style.rating}>{this.props.anime.score}</span>
                <img src="img/ico/eye.svg" />
            </div>
        </a>
    }
}