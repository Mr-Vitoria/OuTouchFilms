import React, { Component } from "react";
import { getAnimeListByTitle } from "../../api/animeService";

import style from "../../assets/css/search.module.scss";
import { BigAnimeCard } from "../shared/bigAnimeCard";

export class SearchContainer extends Component {

    constructor(props) {
        super(props);
        this.state = {
            title: new URLSearchParams(window.location.search).get('title') ?? "",
            animeList: [],
            needLoad: false,
            canLoad: true,
            page: 0,
            count: 10
        }

        this.getAnimeListEvent = this.getAnimeListEvent.bind(this);
        this.beginGetAnimeListEvent = this.beginGetAnimeListEvent.bind(this);
        this.changeInputSearchEvent = this.changeInputSearchEvent.bind(this);
    }

    componentDidMount() {
        this.beginGetAnimeListEvent(this.state.title);

        document.addEventListener("scroll", (ev) => {

            if (window.scrollY > document.getElementsByClassName(style.searchList)[0].scrollHeight / 2) {
                this.beginGetAnimeListEvent(this.state.title, this.state.page);
            }
        });
    }

    async beginGetAnimeListEvent(title, page) {

        this.setState({
            needLoad: true
        });
        this.getAnimeListEvent(title, page);
    }

    changeInputSearchEvent(value) {
        this.setState({
            title: value,
            page: 0,
            animeList: []
        });

        this.beginGetAnimeListEvent(value, 0);
    }

    async getAnimeListEvent(title, page) {

        const result = await getAnimeListByTitle(title, this.state.count, page);

        if (result != undefined && result != false) {
            this.setState({
                animeList: [...this.state.animeList, ...result.animeList],
                needLoad: false,
                canLoad: result.canLoad,
                page: this.state.page + 1
            });
        }
    }

    onlyUnique(value, index, array) {
        return array.findIndex(oldValue => oldValue.id == value.id) === index;
    }

    render() {
        let animeList = this.state.animeList.filter(this.onlyUnique);

        return <section className={style.searchSection}>
            <h3>Список аниме</h3>
            <form>
                <input
                    className="input"
                    type="text"
                    placeholder="Название аниме"
                    defaultValue={this.state.title}
                    onInput={(ev) => {
                        this.changeInputSearchEvent(ev.target.value);
                    }} />
            </form>
            <div className={`${style.searchList}`}>
                {
                        animeList.map((anime, key) => {
                            return <BigAnimeCard anime={anime} key={anime.id} style={style} />
                        })
                }
                {
                    animeList.length <= 0 ?
                        <p>Аниме по вашему запросу не найдено</p>
                        : null
                }
            </div>
        </section>

    }
}