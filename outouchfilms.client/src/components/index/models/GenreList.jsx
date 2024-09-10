import { Component } from "react";
import style from "../../../assets/css/index.module.scss";

export class GenreList extends Component {
    constructor(props) {
        super(props);

        this.state = {
            genreList: props.genreList,
            activeGenre: "",
            onClick: props.onClick,
            title: props.title
        }
    }

    render() {
        return <div className={style.list}>
            <h4>{this.state.title}</h4>
            {this.state.genreList.map((genre, key) => {

                return <p key={`${this.state.title}${key}`} className={this.state.activeGenre == genre.title ? style.active : ""}
                    onClick={(ev) => {
                        this.setState({
                            activeGenre: genre.title
                        });
                        this.state.onClick(genre);
                    }}>
                    {genre.title} <img src="img/ico/arrow_right.svg" />
                </p>
            })}
        </div>
    }
}