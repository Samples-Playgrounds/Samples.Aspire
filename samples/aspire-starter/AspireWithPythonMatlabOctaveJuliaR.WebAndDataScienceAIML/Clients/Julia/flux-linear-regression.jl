using Pkg  # Package to install new packages

# Install packages 
#Pkg.rm("MLJ")
Pkg.add("Flux")
Pkg.add("Plots")

using Flux
using Flux: train!
using Plots

# Generate random data with some weak linear relation
function generate_data(num_elems)
    X = rand(num_elems)
    y = 0.5X .+ rand(num_elems)
    return X, y
end

function reshape_data(X, y)
    X = reduce(hcat, X)
    y = reduce(hcat, y)
    return X, y    
end 

# Set the number of data points
num_elems = 500

x_train, y_train = generate_data(num_elems)
x_test, y_test = generate_data(num_elems)

x_train, y_train = reshape_data(x_train, y_train)
x_test, y_test = reshape_data(x_test, y_test)

model = Dense(1, 1)
ps = Flux.params(model)
loss(x, y) = Flux.Losses.mse(model(x), y)
opt = Descent()

# Get predictions from our model
predict(x) = model(x)

# Compute initial predictions and loss
pred_0 = predict(x_test)
loss_0 = loss(predict(x_test), y_test)

println("Initial loss: $loss_0")

# Zip the train before so we can pass it to the training function
data = [(x_train, y_train)]
n_epochs = 15

for epoch in 1:n_epochs
    train!(loss, ps, data, opt)
    println("Epoch: $epoch, loss: ", loss(predict(x_test), y_test))
end
