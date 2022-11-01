<template>
  <div class="row m-2">
    <RecipeCard v-for="r in recipes" :recipe="r" :key="r.id" data-bs-toggle="modal" data-bs-target="#recipeModal"
      class="selectable" @click="setActiveRecipe(r.id)" />
  </div>
  <RecipeModal />
  <!-- TODO change where to click to bring up modal -->
</template>

<script>
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
    watchEffect(() => {
      if (AppState.account?.email) {
        getFavorites()
      }
    })
    return {
      recipes: computed(() => AppState.recipes),
      setActiveRecipe(recipeId) {
        recipesService.setActiveRecipe(recipeId)
      }
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
