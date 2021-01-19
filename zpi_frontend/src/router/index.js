import { createWebHistory, createRouter } from "vue-router";
import MainPage from "@/components/MainPage.vue";
import Teams from "@/components/Teams";
import Topics from "@/components/Topics";

const routes = [
    {
        path: "/",
        name: "Home",
        component: MainPage,
    },
    {
        path: "/teams",
        name: "Teams",
        component: Teams,
    },
    {
        path: "/topics",
        name: "Topics",
        component: Topics,
    },

    
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;