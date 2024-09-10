import { Component } from "react";

import { AnimeCard } from "../../shared/AnimeCard";

import style from "../../../assets/css/index.module.scss";

export class AnimeGroup extends Component {
    constructor(props) {
        super(props);

        this.state = {
            animeGroup: props.animeGroup
        };
    }

    render() {
        return this.props.animeGroup.animeList.length > 0 ? <div className={`${style.group}`}>
            <h5>{this.props.animeGroup.title}</h5>

            <div className={`${style.carousel}`}>
                {this.props.animeGroup.animeList.map((anime, key) => {
                    return <AnimeCard anime={anime} key={anime.id} />
                })}
            </div>
        </div>
            : null
    }
}