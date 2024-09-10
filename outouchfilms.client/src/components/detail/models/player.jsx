import { Component } from "react";

import style from "../../../assets/css/detail.module.scss";
import { updateUserAnime } from "../../../api/animeService";

export class Player extends Component {

    constructor(props) {
        super(props);

        this.kodikMessageEvent = this.kodikMessageEvent.bind(this);

        this.currentEpisode = -1,
            this.userId = this.props.userId,
            this.animeId = this.props.anime.id,
            this.type = "None"
    }

    componentDidMount() {
        if (window.addEventListener) {
            window.addEventListener('message', this.kodikMessageEvent);
        } else {
            window.attachEvent('onmessage', this.kodikMessageEvent);
        }
    }

    async kodikMessageEvent(message) {

        if (message.data.key == 'kodik_player_play') {

            if (this.userId != undefined && this.type != "Watching") {
                this.type = "Watching";
                await updateUserAnime(this.userId, this.animeId, this.type)
            }
        }


        if (message.data.key == 'kodik_player_current_episode') {
            this.currentEpisode = message.data.value.episode;
            if (this.currentEpisode == this.props.anime.maxEpisodes && this.props.anime.status == "Вышел") {

                if (this.userId != undefined) {
                    this.type = "Completed";
                    await updateUserAnime(this.userId, this.animeId, this.type);
                }
            }

        }
    }

    render() {
        return <section className={`${style.playerSection}`} id="player">
            <h3>{this.props.anime.name} смотреть онлайн</h3>
            <iframe className={`${style.player}`} src={this.props.anime.kodikUrl} frameBorder="0" allowFullScreen={true} allow="autoplay *; fullscreen *">
            </iframe>
        </section>
    }
}