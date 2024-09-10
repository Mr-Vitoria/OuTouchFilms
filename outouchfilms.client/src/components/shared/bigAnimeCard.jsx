import { Component } from "react";

import style from "../../assets/css/profile.module.scss";

export class BigAnimeCard extends Component {
    constructor(props) {
        super(props);

        this.state = {
            style : this.props.style ?? style
        }
    }

    render() {
        return <a href={"/detail?id=" + this.props.anime.id}
            className={`${this.state.style.card}`} style={{ textDecoration: "none" }}
            >
            <img src={this.props.anime.poster} alt="Постер..." />
            <p className={`${this.state.style.title}`}>{this.props.anime.name}</p>
            <p>{this.props.anime.status}</p>
        </a>
    }
}