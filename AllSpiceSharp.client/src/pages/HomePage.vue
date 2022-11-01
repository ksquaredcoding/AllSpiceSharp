<template>
  <div class="row m-2">
    <RecipeCard v-for="r in recipes" :recipe="r" :key="r.id" />
  </div>
  <RecipeModal />
</template>

<script>
import { onAuthLoaded } from "@bcwdev/auth0provider-client";
import { computed } from "@vue/reactivity";
import { onMounted, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import RecipeCard from "../components/RecipeCard.vue";
import RecipeModal from "../components/RecipeModal.vue";
import { favoritesService } from "../services/FavoritesService.js";
import { recipesService } from "../services/RecipesService.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    async function getAllRecipes() {
      try {
        const recipes = await recipesService.getAllRecipes();
        return recipes;
      }
      catch (error) {
        console.error(error);
        Pop.error(error);
      }
    }
    async function getFavorites() {
      try {
        const recipes = await favoritesService.getFavorites();
        return recipes;
      }
      catch (error) {
        console.error(error);
        Pop.error(error);
      }
    }
    onMounted(() => {
      getAllRecipes();
    });
    onAuthLoaded(() => {
      getFavorites()
    })
    return {
      recipes: computed(() => AppState.recipes),
    };
  },
  components: { RecipeCard, RecipeModal }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: 50vw;

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
