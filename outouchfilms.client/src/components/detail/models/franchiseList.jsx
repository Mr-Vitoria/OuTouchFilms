import { Component } from "react";

import { AnimeCard } from "../../shared/AnimeCard";

import style from "../../../assets/css/detail.module.scss";

export class FranchiseList extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return this.props.franchiseList.length > 0 ?
            <section>
                <div className={`${style.group}`}>
                    <h3>Франшиза</h3>

                    <div className={`${style.carousel}`}>
                        {this.props.franchiseList.map((anime, key) => {
                            return <AnimeCard watchAnimeId={this.props.watchAnimeId} style={style} anime={anime} key={anime.id} />
                        })}
                    </div>
                </div>
            </section>
            : null
    }
}