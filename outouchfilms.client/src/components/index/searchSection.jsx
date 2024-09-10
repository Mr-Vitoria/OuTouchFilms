import { Component } from "react";
import { GenreList } from "./models/GenreList";
import { FilterList } from "./models/FilterList";

import style from "../../assets/css/index.module.scss";

export class SearchSection extends Component {

    constructor(props) {
        super(props);
        this.state = {
            genreList: props.genreList,
            activeGenre: null
        }
        this.changeActiveGenre = this.changeActiveGenre.bind(this);
    }

    changeActiveGenre(genre) {
        this.setState({
            activeGenre: genre
        });
    }

    render() {
        return <section className={style.searchSection}>
            <h3>Поиск аниме</h3>
            <div className={style.content}>
                <GenreList onClick={(genre) => {
                    this.changeActiveGenre(genre);
                }} genreList={this.state.genreList} title="Жанр" />
                {

                    this.state.activeGenre == null ?
                        <GenreList genreList={this.state.genreList} title="Поджанр" />
                        : <GenreList genreList={[]} title="Выберите жанр" />
                }

                <FilterList />
            </div>
        </section>
    }

}