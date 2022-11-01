<template>
  <div class="modal fade" id="recipeModal" tabindex="-1" aria-labelledby="recipeModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div class="modal-header d-flex justify-content-between">
          <div>
            <h1 class="modal-title fs-5" id="recipeModalLabel">{{ recipe?.title }}</h1>
            <p><b>{{ recipe?.category }}</b></p>
          </div>
          <div class="d-flex">
            <div v-if="favorited" class="text-success fs-5 selectable mx-1 mt-2" @click="changeFav(recipe?.id)"><i
                class="bi bi-heart-fill"></i>
            </div>
            <div v-else class="text-success fs-5 selectable mx-1 mt-2" @click="changeFav(recipe?.id)"><i
                class="bi bi-heart"></i>
            </div>
            <div>
              <button type="button" class="text-danger btn" aria-label="Delete this recipe"
                @click="deleteRecipe(recipe?.id)"><i class="mdi mdi-delete-forever fs-4 selectable rounded"
                  v-if="account?.id == recipe?.creatorId"></i></button>
            </div>
          </div>
        </div>
        <div class="modal-body">
          <div class="row">
            <div class="col-md-6">
              <div class="card">
                <div class="card-title">
                  <h3 class="m-1 p-1 border-bottom">Recipe Steps</h3>
                </div>
                <div class="card-body">
                  {{ recipe?.instructions }}
                  <form @submit.prevent="handleSubmit" class="d-flex">
                    <input type="text" class="form-control m-2" name="instruction" placeholder="Add Instruction..."
                      required v-model="editable.value">
                    <label for="instruction"></label>
                    <button class="btn btn-dark fs-5 m-2" type="submit">+</button>
                  </form>
                </div>
              </div>
            </div>
            <div class="col-md-6">
              <div class="card">
                <div class="card-title">
                  <h3 class="m-1 p-1 border-bottom">Ingredients</h3>
                </div>
                <div class="card-body" v-if="ingredients">
                  <ul class="list-group list-group-flush">
                    <li class="list-group-item" v-for="n in ingredients" :key="n.id">{{ n.quantity }} {{ n.name }}</li>
                  </ul>
                  <form @submit.prevent="handleSubmit2" class="d-flex">
                    <input type="text" class="form-control m-2" name="quantity" placeholder="Quantity..." required
                      v-model="editable2.quantity">
                    <label for="quantity"></label>
                    <input type="text" class="form-control m-2" name="name" placeholder="Ingredient Name..." required
                      v-model="editable2.name">
                    <label for="name"></label>
                    <button class="btn btn-dark fs-5 m-2" type="submit">+</button>
                  </form>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="modal-footer d-flex justify-content-between">
          <p>Recipe By: {{ recipe?.creator.name }}</p>
          <div>
            <button type="button" class="btn btn-secondary mx-1" data-bs-dismiss="modal">Close</button>
            <button type="button" class="btn btn-primary mx-1">Save changes</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState.js";
import { favoritesService } from "../services/FavoritesService.js";
import { ingredientsService } from "../services/IngredientsService.js";
import { recipesService } from "../services/recipesService.js";
import Pop from "../utils/Pop.js";
import { ref } from "vue";

export default {
  setup() {
    const editable = ref({})
    const editable2 = ref({})
    return {
      editable,
      editable2,
      account: computed(() => AppState.account),
      ingredients: computed(() => AppState.ingredients),
      recipe: computed(() => AppState.activeRecipe),
      favorited: computed(() => AppState.favRecipes.find(f => f.recipeId == AppState.activeRecipe?.id)),
      async changeFav(id) {
        try {
          await favoritesService.changeFav(id)
        } catch (error) {
          console.error('[(UN)FAVORITE RECIPE]', error)
          Pop.error(error.message)
        }
      },
      async handleSubmit() {
        try {
          await recipesService.addInstruction(editable.value)
          editable.value = {}
        } catch (error) {
          console.error('[ADD INSTRUCTION]', error)
          Pop.error(error.message)
        }
      },
      async handleSubmit2() {
        try {
          await ingredientsService.addIngredient(editable2.value)
          editable2.value = {}
        } catch (error) {
          console.error('[ADD INGREDIENT]', error)
          Pop.error(error.message)
        }
      },
    }
  }
}
</script>


<style lang="scss" scoped>

</style>