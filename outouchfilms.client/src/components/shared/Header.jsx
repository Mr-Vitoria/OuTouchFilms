import React, { Component } from "react";
import { getAnimeListByTitle, getRandomAnimeIndex } from "../../api/animeService";

export class Header extends Component {

    constructor(props) {
        super(props);

        this.headerRef = React.createRef();
        this.burgerRef = React.createRef();

        let currentPage = window.location.pathname;

        let activeLink = 'Main';
        
        if(currentPage.includes("profile") || currentPage.includes("login")){
            
            activeLink = 'Profile';
        }
        else if(currentPage.includes("detail")){
            
            activeLink = 'Anime';
        }
        else if(currentPage.includes("search")){
            
            activeLink = 'Search';
        }

        this.state = {
            activePage: activeLink
        }

        this.onRandomClickEvent = this.onRandomClickEvent.bind(this);
        this.searchEvent = this.searchEvent.bind(this);
    }

    async onRandomClickEvent(){
        const result = await getRandomAnimeIndex();

        if(result != false){
            window.location.assign("/detail?id="+ result);
        }
    }

    async searchEvent(title){
        const result = await getAnimeListByTitle(title);
    }

    componentDidMount(){
        window.addEventListener('scroll', (ev) => {
            if(window.scrollY > 75){
                this.headerRef.current.classList.add('bg-dark');
            }
            else{
                this.headerRef.current.classList.remove('bg-dark');
            }
        });
    }

    render() {
        return <header ref={this.headerRef}>
            <a href="/">
                <img className="logo" src="img/logo.png" />
            </a>
            
            <nav>
                <a className={`link ${this.state.activePage == "Main" ? "active" : ""}`} href="/">Главная</a>
                <a className={`link ${this.state.activePage == "Profile" ? "active" : ""}`} href="/login">Профиль</a>
                <a className={`link ${this.state.activePage == "Anime" ? "active" : ""}`} href="/random" onClick={(ev) => {
                    ev.preventDefault();
                    this.onRandomClickEvent();
                }}>Случайное аниме</a>
                <a className={`link ${this.state.activePage == "Search" ? "active" : ""}`} href="/search">Поиск</a>
            </nav>
            
            <div ref={this.burgerRef} className="burger">
                <img src="img/ico/burger.svg"  onClick={(ev) => {
                this.burgerRef.current.classList.toggle('open');
            }}/>
                <div className="content">
                    <a className="link active" href="/">Главная</a>
                    <a className="link" href="/login">Профиль</a>
                    <a className="link" href="/random" onClick={(ev) => {
                    ev.preventDefault();
                    this.onRandomClickEvent();
                }}>Случайное аниме</a>
                    <a className="link" href="/search">Поиск</a>
                </div>
            </div>
            {/* <form action="#">
                <input className="input" type="text" name="filmName" placeholder="Поиcк..." onInput={(ev) => {
                    if(ev.target.value.length > 3)
                        this.searchEvent(ev.target.value);
                }} />
            </form> */}
        </header>
    }
}