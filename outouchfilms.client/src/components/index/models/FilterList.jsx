import { Component } from "react";
import style from "../../../assets/css/index.module.scss";

export class FilterList extends Component {
    constructor(props) {
        super(props);

        this.state = {
            genreList: props.genreList,
            activeGenre: "",
            onClick: props.onClick,
        }
    }

    render() {
        return <div className={`${style.list} ${style.filter}`}>
            <h4>Фильтры</h4>
            <h5>необязательные*</h5>

            <div className={`${style.field}`}>
                <input className={`input`} type="text" placeholder="Название аниме..." name="title" />
            </div>

            <div className={`${style.field}`}>
                <input className={`input`} type="text" placeholder="Название студии..." name="studio" />
            </div>

            <div className={`${style.field}`}>
                <textarea className={`input`} placeholder="Искать в опсании..." name="description"></textarea>
            </div>

            <div className={`${style.field}`}>
                <label>Страна</label>
                <select name="country" className={`input`} >
                    <option value={"Россия"}>Россия</option>
                    <option value={"США"}>США</option>
                    <option value={"Япония"}>Япония</option>
                </select>
            </div>

            <button className={"btn"}>Поиск</button>
        </div>
    }
}