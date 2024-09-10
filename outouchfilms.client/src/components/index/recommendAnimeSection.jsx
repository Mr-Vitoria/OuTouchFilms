import { Component } from "react";

import style from "../../assets/css/index.module.scss";

import { getAnimeGroupList } from "../../api/animeGroupService";

import { AnimeGroup } from "./models/AnimeGroup";

export class RecommendAnimeSection extends Component {

    constructor(props) {
        super(props);

        this.state = {
            count: 5,
            page: 0,
            animeGroupList: [],
            needLoad: false,
            canLoad: true
        };

        this.getAnimeGroupListEvent = this.getAnimeGroupListEvent.bind(this);
        this.beginAnimeLoadEvent = this.beginAnimeLoadEvent.bind(this);
        this.onlyUnique = this.onlyUnique.bind(this);
    }

    componentDidMount() {
        this.beginAnimeLoadEvent();

        document.addEventListener("scroll", (ev) => {

            if(window.scrollY > document.getElementsByClassName(style.filmGroupSection)[0].scrollHeight ){
                this.beginAnimeLoadEvent();
            }
        });
    }

    beginAnimeLoadEvent() {
        if (!this.state.canLoad || this.state.needLoad) {
            return;
        }
        this.setState({
            needLoad: true
        });

        this.getAnimeGroupListEvent();
    }
    async getAnimeGroupListEvent() {
        const result = await getAnimeGroupList(this.state.count, this.state.page);
        if (result != false) {
            this.setState({
                page: result.page + 1,
                animeGroupList: [...this.state.animeGroupList, ...result.groupList],
                needLoad: false,
                canLoad: result.canLoad
            });
        }

    }

    onlyUnique(value, index, array) {
        return array.findIndex(oldValue => oldValue.title == value.title) === index;
    }

    render() {
        let groupList = this.state.animeGroupList.filter(this.onlyUnique);

        return <section className={`${style.filmGroupSection}`}>
            <h3>Рекомендуем к просмотру</h3>
            {
                groupList.length == 0 ?
                    null
                    :
                    <div className={`${style.content}`}>
                        {
                            groupList.map((animeGroup, key) => {
                                return <AnimeGroup animeGroup={animeGroup} key={key} />
                            })}
                    </div>
            }
            {
                this.state.needLoad ?
                    <p>Загрузка</p>
                    : null
            }
        </section>
    }
}