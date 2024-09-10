import { Component } from "react";

import style from "../../../assets/css/profile.module.scss";

import { getUserAnimeList } from "../../../api/animeService";
import { BigAnimeCard } from "../../shared/bigAnimeCard";

export class BookmarkList extends Component {
    constructor(props) {
        super(props);

        this.state = {
            userId: props.userId,
            activeMark: "",

            animeList: [],
            count: 5,
            page: 0,
            needLoad: false,
            canLoad: true
        }

        this.changeBookmarkListEvent = this.changeBookmarkListEvent.bind(this);
        this.beginGetUserAnimeListEvent = this.beginGetUserAnimeListEvent.bind(this);
        this.getUserAnimeListEvent = this.getUserAnimeListEvent.bind(this);
    }

    componentDidMount() {
        this.changeBookmarkListEvent("Completed");
        
        document.addEventListener("scroll", (ev) => {

            if(window.scrollY > document.getElementsByClassName(style.bookmarkList)[0].scrollHeight ){
                this.getUserAnimeListEvent(this.state.activeMark);
            }
        });
    }

    async beginGetUserAnimeListEvent(type, page){
    
        if (!this.state.canLoad || this.state.needLoad) {
            return;
        }
        this.setState({
            needLoad: true
        });

        this.getUserAnimeListEvent(type, page);
    }


    async getUserAnimeListEvent(type, page){

        const result = await getUserAnimeList(this.state.userId, type, this.state.count, page ?? this.state.page);

        if (result != false) {
            this.setState({
                page: result.page + 1,
                animeList: [...this.state.animeList, ...result.userAnimeList],
                needLoad: false,
                canLoad: result.canLoad
            });
        }
        else {
            
        }
    }

    async changeBookmarkListEvent(type) {
        this.setState({
            canLoad: true,
            page: 0,
            activeMark: type,
            animeList: []
        });

        this.beginGetUserAnimeListEvent(type, 0);
    }
    
    onlyUnique(value, index, array) {
        return array.findIndex(oldValue => oldValue.animeId == value.animeId ) === index;
    }

    render() {
        let animeList = this.state.animeList.filter(this.onlyUnique);

        return <section className={`${style.bookmarkList}`}>
            <div className={`${style.header}`}>
                <button className={this.state.activeMark == "Completed" ? style.active : ""} onClick={(ev) => {
                    this.changeBookmarkListEvent("Completed");
                }}>
                    <img src="" />
                    Просмотрено
                </button>
                <button className={this.state.activeMark == "Watching" ? style.active : ""} onClick={(ev) => {
                    this.changeBookmarkListEvent("Watching");
                }}>
                    <img src="" />
                    Смотрю
                </button>
                <button className={this.state.activeMark == "PlanToWatch" ? style.active : ""} onClick={(ev) => {
                    this.changeBookmarkListEvent("PlanToWatch");
                }}>
                    <img src="" />
                    Запланировано
                </button>
                <button className={this.state.activeMark == "Dropped" ? style.active : ""} onClick={(ev) => {
                    this.changeBookmarkListEvent("Dropped");
                }}>
                    <img src="" />
                    Брошено
                </button>
            </div>
            <div className={`${style.body}`}>
                {
                    animeList.length != 0 ?
                        animeList.map((userAnime, key) => {
                            return <BigAnimeCard anime={userAnime.anime} key={userAnime.id} />
                        })
                        : null
                }
            </div>
        </section>
    }
}